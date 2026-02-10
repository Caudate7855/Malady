using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Configs;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "BookSpellListView")]
    public sealed class BookSpellListController : PanelControllerBase<BookSpellListView>, IDisposable, IDragAndDropInput
    {
        private readonly SpellsConfig _spellConfig;
        private readonly ResourcesConfig _resourcesConfig;
        private readonly IPanelManager _panelManager;
        private readonly SpellItem _spellItemPrefab;
        private readonly DragAndDropSystem _dragAndDropSystem;
        private readonly ISpellTipService _spellTipService;

        private readonly List<SpellItem> _wiredItems = new(256);

        public ReactiveCommand<DragAndDropItemBase> OnBeginDrag { get; } = new();
        public ReactiveCommand<Vector2> OnDrag { get; } = new();
        public ReactiveCommand<DragAndDropSlot> OnDrop { get; } = new();
        public ReactiveCommand<Unit> OnEndDrag { get; } = new();

        public BookSpellListController(
            SpellsConfig spellConfig,
            ResourcesConfig resourcesConfig,
            IPanelManager panelManager,
            SpellItem spellItemPrefab,
            DragAndDropSystem dragAndDropSystem,
            ISpellTipService spellTipService)
        {
            _spellConfig = spellConfig;
            _resourcesConfig = resourcesConfig;
            _panelManager = panelManager;
            _spellItemPrefab = spellItemPrefab;
            _dragAndDropSystem = dragAndDropSystem;
            _spellTipService = spellTipService;
        }

        protected override void Initialize()
        {
            Panel.CloseButton.onClick.AddListener(Close);

            _dragAndDropSystem.Register(this);

            _spellTipService.BindCanvas();

            InitSpellsList();
        }

        private void InitSpellsList()
        {
            var slots = new List<SpellSlot>();
            Panel.GetComponentsInChildren(true, slots);

            for (var i = 0; i < slots.Count; i++)
            {
                InitSlot(slots[i]);
            }
        }

        private void InitSlot(SpellSlot slot)
        {
            if (slot == null)
                return;

            if (slot.Spell == null)
                return;

            var spellConfig = _spellConfig.GetSpellConfig(slot.Spell.GetType());
            slot.Init(_spellTipService, _spellConfig, _resourcesConfig, spellConfig);

            if (!slot.IsContainItem)
            {
                if (_spellItemPrefab == null)
                    throw new Exception("BookSpellListController: SpellItem prefab is null");

                var parent = slot.ItemsContainer != null ? slot.ItemsContainer : (RectTransform)slot.transform;

                var item = Object.Instantiate(_spellItemPrefab, parent);
                item.gameObject.SetActive(true);

                FitToParent((RectTransform)item.transform, parent);

                item.Spell = spellConfig.Type;
                item.SpellConfig = spellConfig;
                item.SetIcon(spellConfig.Icon);

                slot.AddItem(item);

                WireItem(item);
            }
            else
            {
                var item = slot.Item;
                if (item != null)
                    WireItem(item);
            }
        }

        private void FitToParent(RectTransform rt, RectTransform parentRt)
        {
            if (rt == null)
                return;

            if (parentRt == null)
                return;

            var le = rt.GetComponent<LayoutElement>();
            if (le != null)
                le.ignoreLayout = true;

            Canvas.ForceUpdateCanvases();
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRt);

            rt.anchorMin = new Vector2(0.5f, 0.5f);
            rt.anchorMax = new Vector2(0.5f, 0.5f);
            rt.anchoredPosition = Vector2.zero;

            rt.sizeDelta = parentRt.rect.size;
        }

        private void WireItem(SpellItem item)
        {
            if (item == null)
                return;

            for (var i = 0; i < _wiredItems.Count; i++)
            {
                if (_wiredItems[i] == item)
                    return;
            }

            _wiredItems.Add(item);

            item.BeginDrag += OnItemBeginDrag;
            item.Drag += OnItemDrag;
            item.EndDrag += OnItemEndDrag;
        }

        private void UnwireAllItems()
        {
            for (var i = 0; i < _wiredItems.Count; i++)
            {
                var item = _wiredItems[i];
                if (item == null)
                    continue;

                item.BeginDrag -= OnItemBeginDrag;
                item.Drag -= OnItemDrag;
                item.EndDrag -= OnItemEndDrag;
            }

            _wiredItems.Clear();
        }

        private void OnItemBeginDrag(DragAndDropItemBase item, UnityEngine.EventSystems.PointerEventData e)
        {
            OnBeginDrag.Execute(item);
        }

        private void OnItemDrag(DragAndDropItemBase item, UnityEngine.EventSystems.PointerEventData e)
        {
            OnDrag.Execute(e.position);
        }

        private void OnItemEndDrag(DragAndDropItemBase item, UnityEngine.EventSystems.PointerEventData e)
        {
            OnEndDrag.Execute(Unit.Default);

            var spellItem = item as SpellItem;
            if (spellItem == null)
                return;

            var rt = (RectTransform)spellItem.transform;
            var parentRt = rt.parent as RectTransform;
            if (parentRt == null)
                return;

            FitToParent(rt, parentRt);
        }

        public void Dispose()
        {
            Panel.CloseButton.onClick.RemoveListener(Close);

            if (_dragAndDropSystem != null)
            {
                _dragAndDropSystem.Unregister(this);
            }

            UnwireAllItems();

            OnBeginDrag.Dispose();
            OnDrag.Dispose();
            OnDrop.Dispose();
            OnEndDrag.Dispose();
        }
    }
}