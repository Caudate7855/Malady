using System;
using System.Collections.Generic;
using DG.Tweening;
using Itibsoft.PanelManager;
using R3;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "InventoryView")]
    public class InventoryController : PanelControllerBase<InventoryView>, IDragAndDropInput, IDisposable
    {
        [Inject] private ItemsConfig _itemsConfig;
        [Inject] private StatSystem _statSystem;
        [Inject] private InventoryItem _baseItem;
        [Inject] private IPanelManager _panelManager;
        [Inject] private DragAndDropSystem _dragAndDropSystem;

        private Button _statsWindowButton;
        private bool _isStatsWindowOpened;

        private RectTransform _statsWindowRectTransform;
        private RectTransform _defaultStatsWindowRectTransform;

        private List<InventorySlot> _inventorySlots;
        private RectTransform _itemsContainer;
        private RectTransform _statsContainer;

        private readonly List<DragAndDropItemBase> _wiredItems = new(64);

        public RectTransform DragRoot { get; private set; }

        public ReactiveCommand<DragAndDropItemBase> OnBeginDrag { get; } = new();
        public ReactiveCommand<Vector2> OnDrag { get; } = new();
        public ReactiveCommand<DragAndDropSlot> OnDrop { get; } = new();
        public ReactiveCommand<Unit> OnEndDrag { get; } = new();

        protected override void Initialize()
        {
            DragRoot = _panelManager.PanelDispatcher.Canvas.GetComponent<RectTransform>();

            _dragAndDropSystem.Register(this);

            _statsWindowButton = Panel.StatsButton;
            _inventorySlots = Panel.InventorySlots;
            _itemsContainer = Panel.ItemsContainer;
            _statsContainer = Panel.StatsContainer;

            WireSlots();

            InitializeTestItems();

            _statsWindowButton.onClick.AddListener(OnStatsButtonClicked);
            _statsWindowRectTransform = Panel.StatsWindowRectTransform;
            _defaultStatsWindowRectTransform = _statsWindowRectTransform;

            InitializeStatsView();
        }

        public void Dispose()
        {
            if (_dragAndDropSystem != null)
            {
                _dragAndDropSystem.Unregister(this);
            }

            UnwireSlots();
            UnwireItems();

            OnBeginDrag.Dispose();
            OnDrag.Dispose();
            OnDrop.Dispose();
            OnEndDrag.Dispose();
        }

        private void WireSlots()
        {
            for (var i = 0; i < _inventorySlots.Count; i++)
            {
                var slot = _inventorySlots[i];
                slot.Dropped += OnSlotDropped;
            }
        }

        private void UnwireSlots()
        {
            if (_inventorySlots == null)
            {
                return;
            }

            for (var i = 0; i < _inventorySlots.Count; i++)
            {
                var slot = _inventorySlots[i];
                slot.Dropped -= OnSlotDropped;
            }
        }

        private void OnSlotDropped(DragAndDropSlot slot, UnityEngine.EventSystems.PointerEventData e)
        {
            OnDrop.Execute(slot);
        }

        private void InitializeTestItems()
        {
            var item0 = _inventorySlots[0].CreateNewItem(_baseItem, _itemsContainer.gameObject);
            var item1 = _inventorySlots[1].CreateNewItem(_baseItem, _itemsContainer.gameObject);
            var item2 = _inventorySlots[2].CreateNewItem(_baseItem, _itemsContainer.gameObject);

            WireItem(item0);
            WireItem(item1);
            WireItem(item2);
        }

        private void WireItem(DragAndDropItemBase item)
        {
            if (item == null)
            {
                return;
            }

            item.BeginDrag += OnItemBeginDrag;
            item.Drag += OnItemDrag;
            item.EndDrag += OnItemEndDrag;

            _wiredItems.Add(item);
        }

        private void UnwireItems()
        {
            for (var i = 0; i < _wiredItems.Count; i++)
            {
                var item = _wiredItems[i];
                if (item == null)
                {
                    continue;
                }

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
        }

        private void InitializeStatsView()
        {
        }

        public void UpdateStatView(StatType type, float newValue)
        {
        }

        private void OnStatsButtonClicked()
        {
            _isStatsWindowOpened = !_isStatsWindowOpened;

            if (_isStatsWindowOpened)
            {
                OpenStatsWindow();
            }
            else
            {
                CloseStatsWindow();
            }
        }

        private void OpenStatsWindow()
        {
            _statsWindowRectTransform.DOAnchorPosX(0f, 0.5f);
        }

        private void CloseStatsWindow()
        {
            _statsWindowRectTransform.DOAnchorPosX(300f, 0.5f);
        }
    }
}