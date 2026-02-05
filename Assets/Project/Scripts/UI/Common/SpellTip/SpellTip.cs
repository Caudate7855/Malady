using System.Collections.Generic;
using Project.Scripts.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public sealed class SpellTip : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;

        [SerializeField] private RectTransform _resourceObjectContainer;
        [SerializeField] private SpellTipResource _spellTipResourcePrefab;

        [SerializeField] private Vector2 _offset = new(18f, -18f);

        private readonly List<SpellTipResource> _spellTipResources = new();

        private RectTransform _rectTransform;
        private RectTransform _canvasRect;
        private Canvas _canvas;

        private bool _isOpen;
        private bool _layoutDirty;

        private void Awake()
        {
            _rectTransform = (RectTransform)transform;

            var cg = GetComponent<CanvasGroup>();
            if (cg == null)
                cg = gameObject.AddComponent<CanvasGroup>();

            cg.blocksRaycasts = false;
            cg.interactable = false;

            var graphics = GetComponentsInChildren<Graphic>(true);
            for (var i = 0; i < graphics.Length; i++)
                graphics[i].raycastTarget = false;

            gameObject.SetActive(false);
        }

        public void Init(Canvas canvas)
        {
            _canvas = canvas;
            _canvasRect = (RectTransform)canvas.transform;
        }

        public void Open()
        {
            _isOpen = true;
            gameObject.SetActive(true);

            _layoutDirty = true;
            UpdatePosition();
        }

        public void Close()
        {
            _isOpen = false;
            gameObject.SetActive(false);
        }

        public void SetInfo(SpellConfig spellConfig, ResourcesConfig resourceConfig)
        {
            _image.sprite = spellConfig.Icon;
            _name.text = spellConfig.Name;
            _description.text = spellConfig.Description;

            FillResourceObjects(spellConfig.Cost, resourceConfig);

            _layoutDirty = true;

            if (_isOpen)
                UpdatePosition();
        }

        private void Update()
        {
            if (_isOpen == false)
                return;

            UpdatePosition();
        }

        private void UpdatePosition()
        {
            if (_canvasRect == null)
                return;

            if (_layoutDirty)
            {
                Canvas.ForceUpdateCanvases();
                LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
                _layoutDirty = false;
            }

            var scale = _canvas != null ? _canvas.scaleFactor : 1f;

            var sizeCanvas = _rectTransform.rect.size;
            var sizePx = sizeCanvas * scale;

            var pivot = _rectTransform.pivot;

            var screen = (Vector2)Input.mousePosition + _offset;

            var minX = sizePx.x * pivot.x;
            var maxX = Screen.width - sizePx.x * (1f - pivot.x);

            var minY = sizePx.y * pivot.y;
            var maxY = Screen.height - sizePx.y * (1f - pivot.y);

            screen.x = Mathf.Clamp(screen.x, minX, maxX);
            screen.y = Mathf.Clamp(screen.y, minY, maxY);

            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvasRect, screen, null, out var world))
                _rectTransform.position = world;
        }

        private void FillResourceObjects(Dictionary<ResourceType, float> cost, ResourcesConfig resourceConfig)
        {
            var used = 0;

            foreach (var kv in cost)
            {
                SpellTipResource view;

                if (used < _spellTipResources.Count)
                {
                    view = _spellTipResources[used];
                    view.gameObject.SetActive(true);
                }
                else
                {
                    view = Instantiate(_spellTipResourcePrefab, _resourceObjectContainer);
                    _spellTipResources.Add(view);
                }

                var sprite = resourceConfig.GetResourceConfig(kv.Key).Icon;
                view.SetInfo(sprite, kv.Value);
                used++;
            }

            for (var i = used; i < _spellTipResources.Count; i++)
                _spellTipResources[i].gameObject.SetActive(false);
        }
    }
}
