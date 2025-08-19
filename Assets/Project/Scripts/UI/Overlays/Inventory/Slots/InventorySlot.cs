using UnityEngine;

namespace Project.Scripts.Overlays.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        public RectTransform ItemsContainer;
        
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
            Item = item;
            IsContainItem = true;
            Item.CurrentInventorySlot = this;
            SetItem(item);
        }

        public void OnDragItem()
        {
            Item.GetComponent<RectTransform>().SetParent(ItemsContainer, false); 
        }
        
        public void RemoveItem()
        {
            Item.GetComponent<RectTransform>().SetParent(ItemsContainer, false); 
            Item = null;
            IsContainItem = false;
        }

        private void SetItem(InventoryItem item)
        {
            item.GetComponent<RectTransform>().SetParent(GetComponent<RectTransform>(), false); 
            item.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        }
    }
}