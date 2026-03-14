using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class InventorySlot : DragAndDropSlot, IPointerEnterHandler, IPointerExitHandler
    {
        [field: SerializeField] public bool IsEquipmentSlot { get; private set; }
        [field: SerializeField] public ItemType AllowedItemType { get; private set; }
        [field: SerializeField] public Image EmptySlotBackgroundImage { get; private set; }
        
        private ITipService _tipService;
        private EquipmentSystem _equipmentSystem;

        public new InventoryItem InventoryItem => (InventoryItem)Item;
        public bool IsContainItem => base.HasItem;
        protected EquipmentSystem EquipmentSystem => _equipmentSystem;

        private void Start()
        {
            ChangeBorderVisibility(false);
        }

        public void Init(ITipService tipService, EquipmentSystem equipmentSystem)
        {
            _tipService = tipService;
            _equipmentSystem = equipmentSystem;
        }

        public InventoryItem CreateNewItem(InventoryItem itemPrefab, GameObject parentObject, ItemData itemData)
        {
            var parent = Content != null ? Content : parentObject.GetComponent<RectTransform>();
            var item = Instantiate(itemPrefab, parent);

            item.ItemData = itemData;

            var rt = item.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;

            AddItem(item);

            return item;
        }
        
        public virtual bool CanAccept(ItemData itemData)
        {
            if (itemData == null)
            {
                return false;
            }

            if (!IsEquipmentSlot)
            {
                return true;
            }

            return itemData.Type == AllowedItemType;
        }

        public virtual void AddItem(InventoryItem item)
        {
            if (item == null)
            {
                return;
            }

            item.CurrentInventorySlot = this;
            SetItem(item);

            var rt = item.GetComponent<RectTransform>();
            rt.anchoredPosition = Vector2.zero;

            if (IsPointerOverSlot())
            {
                ChangeBorderVisibility(true);

                if (_tipService != null)
                {
                    _tipService.ShowItemTip(item.ItemData);
                }
            }
            else
            {
                ChangeBorderVisibility(false);
            }

            OnItemPlaced(item);
        }

        public override DragAndDropItemBase ClearItem()
        {
            var item = base.ClearItem() as InventoryItem;

            if (item != null)
            {
                item.CurrentInventorySlot = null;
                OnItemRemoved(item);
            }

            return item;
        }

        protected virtual void OnItemPlaced(InventoryItem item)
        {
        }

        protected virtual void OnItemRemoved(InventoryItem item)
        {
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ChangeBorderVisibility(true);
            
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
            ChangeBorderVisibility(false);
            
            if (_tipService == null)
            {
                return;
            }

            _tipService.Hide();
        }
        
        private bool IsPointerOverSlot()
        {
            return RectTransformUtility.RectangleContainsScreenPoint(
                (RectTransform)transform,
                Input.mousePosition,
                Camera.main);
        }
    }
}