using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Configs;
using R3;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public sealed class MainUIController : PanelControllerBase<MainUIView>, IDisposable, IDragAndDropInput
    {
        [Inject] private DragAndDropSystem _dragAndDropSystem;
        [Inject] private IPanelManager _panelManager;
        [Inject] private ITipService _tipService;
        [Inject] private SpellsConfig _spellsConfig;
        [Inject] private ResourcesConfig _resourcesConfig;

        private readonly List<SpellItem> _wiredItems = new(64);

        public ReactiveCommand<DragAndDropItemBase> OnBeginDrag { get; } = new();
        public ReactiveCommand<Vector2> OnDrag { get; } = new();
        public ReactiveCommand<DragAndDropSlot> OnDrop { get; } = new();
        public ReactiveCommand<Unit> OnEndDrag { get; } = new();

        protected override void Initialize()
        {
            _dragAndDropSystem.Register(this);

            _tipService.BindCanvas();

            InitSpellSlots();
            WireSpellItems();
        }

        private void InitSpellSlots()
        {
            var slots = new List<SpellSlot>();
            Panel.GetComponentsInChildren(true, slots);

            for (var i = 0; i < slots.Count; i++)
            {
                var slot = slots[i];
                if (slot == null)
                {
                    continue;
                }

                slot.Init(_tipService, _spellsConfig, _resourcesConfig);
            }
        }

        public void WireSpellItem(SpellItem item)
        {
            WireItem(item);
        }

        private void WireSpellItems()
        {
            var items = new List<SpellItem>();
            Panel.GetComponentsInChildren(true, items);

            for (var i = 0; i < items.Count; i++)
            {
                WireItem(items[i]);
            }
        }

        private void WireItem(SpellItem item)
        {
            if (item == null)
            {
                return;
            }

            for (var i = 0; i < _wiredItems.Count; i++)
            {
                if (_wiredItems[i] == item)
                {
                    return;
                }
            }

            _wiredItems.Add(item);

            item.BeginDrag += OnItemBeginDrag;
            item.Drag += OnItemDrag;
            item.EndDrag += OnItemEndDrag;
        }

        private void UnwireAll()
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

        public void OnPlayerSpellButtonClicked(int index)
        {
        }

        public void Dispose()
        {
            if (_dragAndDropSystem != null)
            {
                _dragAndDropSystem.Unregister(this);
            }

            UnwireAll();

            _tipService?.Hide();

            OnBeginDrag.Dispose();
            OnDrag.Dispose();
            OnDrop.Dispose();
            OnEndDrag.Dispose();
        }
    }
}