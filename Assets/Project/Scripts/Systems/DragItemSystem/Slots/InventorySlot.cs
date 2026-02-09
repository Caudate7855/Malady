using UnityEngine;

namespace Project.Scripts
{
    public sealed class InventorySlot : DragAndDropSlot
    {
        public RectTransform ItemsContainer;

        public SlotType SlotType;

        public new InventoryItem Item => (InventoryItem)base.Item;
        public bool IsContainItem => base.HasItem;

        public InventoryItem CreateNewItem(InventoryItem itemPrefab, GameObject parentObject)
        {
            var parent = ItemsContainer != null ? ItemsContainer : parentObject.GetComponent<RectTransform>();
            var item = Object.Instantiate(itemPrefab, parent);

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
            base.SetItem(item);

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
    }
}