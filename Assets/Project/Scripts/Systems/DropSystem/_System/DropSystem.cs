using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    public class DropSystem : IInitializable, ITickable, IDisposable
    {
        private const string ContainerName = "DropsContainer";
        private const float EpsilonSqr = 0.0001f;
        private const float GoldenAngle = 2.39996323f;
        private const int MaxTries = 260;
        private const float OverlapWeight = 100000f;

        private readonly IPanelManager _panelManager;
        private readonly PlayerController _playerController;

        private readonly Canvas _canvas;
        private readonly DropItemUIView _dropItemUIViewPrefab;

        private readonly List<Entry> _entries = new();
        private readonly List<Entry> _visibleEntries = new(64);
        private readonly List<Rect> _occupied = new(64);

        private RectTransform _containerRect;
        private Camera _worldCamera;
        private Camera _eventCamera;

        private InventoryController _inventoryController;

        private float _spacingK = 0.28f;
        private float _collisionK = 0.78f;

        public DropSystem(IPanelManager panelManager, PlayerController playerController, DropItemUIView dropItemUIViewPrefab)
        {
            _panelManager = panelManager;
            _playerController = playerController;

            _canvas = panelManager.PanelDispatcher.Canvas;
            _dropItemUIViewPrefab = dropItemUIViewPrefab;
        }

        public void Initialize()
        {
            EnsureContainer();
            _worldCamera = ResolveWorldCamera();
            _inventoryController = _panelManager.LoadPanel<InventoryController>();
        }

        public void Register(WorldDropItem drop)
        {
            if (drop == null)
            {
                return;
            }

            EnsureContainer();

            var view = UnityEngine.Object.Instantiate(_dropItemUIViewPrefab, _containerRect);
            view.Image.sprite = drop.Sprite;

            var rt = (RectTransform)view.transform;
            if (rt.rect.size.sqrMagnitude <= EpsilonSqr)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(rt);
            }

            var entry = new Entry
            {
                Drop = drop,
                World = drop.transform,
                View = view
            };

            view.SetOnClick(() =>
            {
                if (entry.Drop == null)
                {
                    return;
                }

                _playerController.TryMoveToPoint(entry.Drop.transform.position, entry.Drop, true);
            });

            _entries.Add(entry);
            drop.PickedUp += OnPickedUp;
        }

        public bool Despawn(WorldDropItem drop)
        {
            if (drop == null)
            {
                return false;
            }

            return Despawn(drop.transform);
        }

        public bool Despawn(Transform worldTarget)
        {
            if (worldTarget == null)
            {
                return false;
            }

            for (var i = _entries.Count - 1; i >= 0; i--)
            {
                var e = _entries[i];
                if (e.World != worldTarget)
                {
                    continue;
                }

                Unwire(e);

                _entries.RemoveAt(i);
                return true;
            }

            return false;
        }

        public void Tick()
        {
            if (_worldCamera == null)
            {
                _worldCamera = ResolveWorldCamera();
            }

            _eventCamera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;

            _visibleEntries.Clear();

            for (var i = _entries.Count - 1; i >= 0; i--)
            {
                var e = _entries[i];

                if (e.World == null || e.View == null || e.Drop == null)
                {
                    Unwire(e);
                    _entries.RemoveAt(i);
                    continue;
                }

                if (!TryWorldToAnchored(e.World.position, out var anchored))
                {
                    DisableView(e.View);
                    continue;
                }

                EnableView(e.View);

                e.Projected = anchored;
                e.Desired = anchored;

                var rt = (RectTransform)e.View.transform;
                e.Size = GetSize(rt);

                _visibleEntries.Add(e);
            }

            if (_visibleEntries.Count > 1)
            {
                PlaceNonOverlapping();
            }

            for (var i = 0; i < _visibleEntries.Count; i++)
            {
                var e = _visibleEntries[i];
                ((RectTransform)e.View.transform).anchoredPosition = e.Desired;
            }
        }

        public void Dispose()
        {
            for (var i = _entries.Count - 1; i >= 0; i--)
            {
                Unwire(_entries[i]);
            }

            _entries.Clear();
            _visibleEntries.Clear();
            _occupied.Clear();
        }

        private void OnPickedUp(WorldDropItem drop)
        {
            if (drop == null)
            {
                return;
            }

            var added = _inventoryController != null && _inventoryController.TryAddItem(drop.ItemData);
            if (!added)
            {
                return;
            }

            drop.PickedUp -= OnPickedUp;

            Despawn(drop.transform);
            UnityEngine.Object.Destroy(drop.gameObject);
        }

        private void Unwire(Entry e)
        {
            if (e.Drop != null)
            {
                e.Drop.PickedUp -= OnPickedUp;
            }

            if (e.View != null)
            {
                e.View.ClearOnClick();
                UnityEngine.Object.Destroy(e.View.gameObject);
            }

            e.Drop = null;
            e.World = null;
            e.View = null;
        }

        private void PlaceNonOverlapping()
        {
            _visibleEntries.Sort(CompareByScreenPosition);

            _occupied.Clear();

            for (var i = 0; i < _visibleEntries.Count; i++)
            {
                var e = _visibleEntries[i];
                e.Desired = FindFreeSpot(e);
                _occupied.Add(MakeRect(e.Desired, e.Size));
            }
        }

        private int CompareByScreenPosition(Entry a, Entry b)
        {
            var byY = b.Projected.y.CompareTo(a.Projected.y);
            if (byY != 0)
            {
                return byY;
            }

            return a.Projected.x.CompareTo(b.Projected.x);
        }

        private Vector2 FindFreeSpot(Entry e)
        {
            var basePos = e.Projected;
            var spacing = Mathf.Max(e.Size.x, e.Size.y) * _spacingK;

            var bestPos = basePos;
            var bestScore = float.MaxValue;

            for (var k = 0; k < MaxTries; k++)
            {
                var candidate = basePos + SpiralOffset(k, spacing);

                var rect = MakeRect(candidate, e.Size);
                var overlaps = CountIntersections(rect);

                if (overlaps == 0)
                {
                    return candidate;
                }

                var dist = (candidate - basePos).sqrMagnitude;
                var score = overlaps * OverlapWeight + dist;

                if (score < bestScore)
                {
                    bestScore = score;
                    bestPos = candidate;
                }
            }

            return bestPos;
        }

        private Vector2 SpiralOffset(int k, float spacing)
        {
            if (k == 0)
            {
                return Vector2.zero;
            }

            var angle = k * GoldenAngle;
            var radius = spacing * Mathf.Sqrt(k);

            return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        private int CountIntersections(Rect rect)
        {
            var count = 0;

            for (var i = 0; i < _occupied.Count; i++)
            {
                if (rect.Overlaps(_occupied[i]))
                {
                    count++;
                }
            }

            return count;
        }

        private Rect MakeRect(Vector2 center, Vector2 size)
        {
            var half = size * 0.5f * _collisionK;
            return new Rect(center - half, half * 2f);
        }

        private Vector2 GetSize(RectTransform rt)
        {
            var size = rt.rect.size;

            if (size.sqrMagnitude > EpsilonSqr)
            {
                return size;
            }

            return rt.sizeDelta;
        }

        private void EnsureContainer()
        {
            if (_containerRect != null)
            {
                return;
            }

            var root = _canvas.transform;
            var existing = root.Find(ContainerName);

            if (existing != null)
            {
                _containerRect = existing as RectTransform;
                return;
            }

            var go = new GameObject(ContainerName, typeof(RectTransform));
            go.transform.SetParent(root, false);
            go.transform.SetAsFirstSibling();

            var rtNew = (RectTransform)go.transform;
            rtNew.anchorMin = Vector2.zero;
            rtNew.anchorMax = Vector2.one;
            rtNew.anchoredPosition = Vector2.zero;
            rtNew.sizeDelta = Vector2.zero;

            _containerRect = rtNew;
        }

        private Camera ResolveWorldCamera()
        {
            if (_canvas.renderMode != RenderMode.ScreenSpaceOverlay && _canvas.worldCamera != null)
            {
                return _canvas.worldCamera;
            }

            return Camera.main;
        }

        private bool TryWorldToAnchored(Vector3 worldPosition, out Vector2 anchoredPosition)
        {
            anchoredPosition = default;

            if (_worldCamera == null)
            {
                return false;
            }

            var vp = _worldCamera.WorldToViewportPoint(worldPosition);

            if (vp.z <= 0f)
            {
                return false;
            }

            if (vp.x < 0f || vp.x > 1f || vp.y < 0f || vp.y > 1f)
            {
                return false;
            }

            var sp = _worldCamera.WorldToScreenPoint(worldPosition);

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_containerRect, sp, _eventCamera, out var lp))
            {
                return false;
            }

            anchoredPosition = lp;
            return true;
        }

        private void DisableView(DropItemUIView view)
        {
            if (view.gameObject.activeSelf)
            {
                view.gameObject.SetActive(false);
            }
        }

        private void EnableView(DropItemUIView view)
        {
            if (!view.gameObject.activeSelf)
            {
                view.gameObject.SetActive(true);
            }
        }

        private class Entry
        {
            public WorldDropItem Drop;

            public Transform World;
            public DropItemUIView View;

            public Vector2 Projected;
            public Vector2 Desired;
            public Vector2 Size;
        }
    }
}