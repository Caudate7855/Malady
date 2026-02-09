using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    public sealed class DragAndDropSystem : IInitializable, IDisposable
    {
        private readonly IPanelManager _panelManager;

        private readonly Dictionary<IDragAndDropInput, CompositeDisposable> _subscriptions = new();
        private readonly List<RaycastResult> _raycastResults = new(32);

        private RectTransform _dragRoot;
        private Canvas _canvas;
        private Camera _uiCamera;
        private GraphicRaycaster _raycaster;
        private EventSystem _eventSystem;

        private DragAndDropItemBase _dragItem;
        private DragAndDropSlot _fromSlot;

        private Vector2 _lastScreenPos;

        public DragAndDropSystem(IPanelManager panelManager)
        {
            _panelManager = panelManager;
        }

        public void Initialize()
        {
            _canvas = _panelManager.PanelDispatcher.Canvas;
            _dragRoot = _canvas.GetComponent<RectTransform>();

            if (_dragRoot == null)
            {
                throw new Exception("DragAndDropSystem: drag root is null");
            }

            _raycaster = _canvas.GetComponent<GraphicRaycaster>();
            if (_raycaster == null)
            {
                throw new Exception("DragAndDropSystem: GraphicRaycaster not found on Canvas");
            }

            _eventSystem = EventSystem.current;
            if (_eventSystem == null)
            {
                throw new Exception("DragAndDropSystem: EventSystem.current is null");
            }

            _uiCamera = _canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : _canvas.worldCamera;
        }

        public void Dispose()
        {
            foreach (var kv in _subscriptions)
            {
                kv.Value.Dispose();
            }

            _subscriptions.Clear();
        }

        public void Register(IDragAndDropInput input)
        {
            if (input == null)
            {
                throw new Exception("DragAndDropSystem: input is null");
            }

            if (_subscriptions.ContainsKey(input))
            {
                return;
            }

            var d = new CompositeDisposable();
            _subscriptions.Add(input, d);

            input.OnBeginDrag.Subscribe(BeginDrag).AddTo(d);
            input.OnDrag.Subscribe(Drag).AddTo(d);
            input.OnEndDrag.Subscribe(_ => EndDrag()).AddTo(d);
        }

        public void Unregister(IDragAndDropInput input)
        {
            if (input == null)
            {
                return;
            }

            if (_subscriptions.TryGetValue(input, out var d))
            {
                d.Dispose();
                _subscriptions.Remove(input);
            }
        }

        private void BeginDrag(DragAndDropItemBase item)
        {
            if (item == null)
            {
                return;
            }

            _dragItem = item;
            _fromSlot = item.Slot;

            _lastScreenPos = Input.mousePosition;

            _dragItem.transform.SetParent(_dragRoot, false);

            Drag(_lastScreenPos);
        }

        private void Drag(Vector2 screenPos)
        {
            if (_dragItem == null)
            {
                return;
            }

            _lastScreenPos = screenPos;

            var rt = (RectTransform)_dragItem.transform;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(_dragRoot, screenPos, _uiCamera, out var localPoint);
            rt.anchoredPosition = localPoint;
        }

        private void EndDrag()
        {
            if (_dragItem == null)
            {
                return;
            }

            if (_fromSlot == null)
            {
                ClearDrag();
                return;
            }

            var targetSlot = RaycastSlot(_lastScreenPos);

            if (targetSlot == null)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            if (targetSlot == _fromSlot)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            if (targetSlot.Kind != _fromSlot.Kind)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            if (_fromSlot is InventorySlot fromInv && targetSlot is InventorySlot toInv)
            {
                EndDragInventory(fromInv, toInv);
                return;
            }

            EndDragSwap(targetSlot);
        }

        private DragAndDropSlot RaycastSlot(Vector2 screenPos)
        {
            _raycastResults.Clear();

            var ped = new PointerEventData(_eventSystem)
            {
                position = screenPos
            };

            _raycaster.Raycast(ped, _raycastResults);

            for (var i = 0; i < _raycastResults.Count; i++)
            {
                var go = _raycastResults[i].gameObject;
                if (go == null)
                {
                    continue;
                }

                if (_dragItem != null && go.transform.IsChildOf(_dragItem.transform))
                {
                    continue;
                }

                var slot = go.GetComponentInParent<DragAndDropSlot>();
                if (slot != null)
                {
                    return slot;
                }
            }

            return null;
        }

        private void EndDragInventory(InventorySlot fromSlot, InventorySlot toSlot)
        {
            var invItem = _dragItem as InventoryItem;
            if (invItem == null)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            var targetOk = toSlot.SlotType == SlotType.Inventory || toSlot.SlotType == invItem.SlotType;
            if (!targetOk)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            if (toSlot.IsContainItem)
            {
                ReturnToFromSlot();
                ClearDrag();
                return;
            }

            fromSlot.ClearItem();
            toSlot.SetItem(invItem);

            var rt = (RectTransform)invItem.transform;
            rt.anchoredPosition = Vector2.zero;

            invItem.CurrentInventorySlot = toSlot;

            ClearDrag();
        }

        private void EndDragSwap(DragAndDropSlot targetSlot)
        {
            var targetItem = targetSlot.ClearItem();
            var fromItem = _fromSlot.ClearItem();

            targetSlot.SetItem(fromItem);
            if (fromItem != null)
            {
                var rtA = (RectTransform)fromItem.transform;
                rtA.anchoredPosition = Vector2.zero;
            }

            _fromSlot.SetItem(targetItem);
            if (targetItem != null)
            {
                var rtB = (RectTransform)targetItem.transform;
                rtB.anchoredPosition = Vector2.zero;
            }

            ClearDrag();
        }

        private void ReturnToFromSlot()
        {
            if (_fromSlot != null && _dragItem != null)
            {
                _fromSlot.SetItem(_dragItem);

                var rt = (RectTransform)_dragItem.transform;
                rt.anchoredPosition = Vector2.zero;
            }
        }

        private void ClearDrag()
        {
            _dragItem = null;
            _fromSlot = null;
        }
    }
}