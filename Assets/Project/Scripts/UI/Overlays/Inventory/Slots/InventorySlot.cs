using UnityEngine;

namespace Project.Scripts.Overlays.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public SlotType SlotType;
        public InventoryItem Item;
        public bool IsContainItem;

        public void CreateNewItem(InventoryItem item, GameObject parentObject)
        {
            Item = Instantiate(item, parentObject.transform);

            Item.GetComponent<RectTransform>().position = Vector3.zero;

            AddItem(Item);
        }
        
        public void AddItem(InventoryItem item)
        {
            SetItem(item);
            Item = item;
            IsContainItem = true;
            Item.CurrentInventorySlot = this;
        }

        public void RemoveItem()
        {
            Item = null;
            IsContainItem = false;
        }

        private void SetItem(InventoryItem item)
        {
            item.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
            item.CurrentInventorySlot = this;
        }
    }
}