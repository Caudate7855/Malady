using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts
{
    public sealed class InventorySlot : DragAndDropSlot, IPointerEnterHandler, IPointerExitHandler
    {
        public RectTransform ItemsContainer;

        public InventorySlotType _inventorySlotType;

        private ITipService _tipService;

        public new InventoryItem InventoryItem => (InventoryItem)Item;
        public bool IsContainItem => base.HasItem;

        public void Init(ITipService tipService)
        {
            _tipService = tipService;
        }

        public InventoryItem CreateNewItem(InventoryItem itemPrefab, GameObject parentObject)
        {
            var parent = ItemsContainer != null ? ItemsContainer : parentObject.GetComponent<RectTransform>();
            var item = Instantiate(itemPrefab, parent);

            var rt = item.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;

            AddItem(item);

            return item;
        }

        public void AddItem(InventoryItem item)
        {
            if (item == null)
            {
                return;
            }

            item.CurrentInventorySlot = this;
            SetItem(item);

            var rt = item.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;
        }

        public void RemoveItemToContainer()
        {
            if (!HasItem)
            {
                return;
            }

            var item = (InventoryItem)ClearItem();

            if (ItemsContainer != null)
            {
                var rt = item.GetComponent<RectTransform>();
                rt.SetParent(ItemsContainer, false);
                rt.anchoredPosition = Vector2.zero;
            }

            item.CurrentInventorySlot = null;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_tipService == null)
            {
                return;
            }

            var item = InventoryItem;
            if (item == null)
            {
                return;
            }

            _tipService.ShowItemTip(item.ItemData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_tipService == null)
            {
                return;
            }

            _tipService.Hide();
        }
    }
}