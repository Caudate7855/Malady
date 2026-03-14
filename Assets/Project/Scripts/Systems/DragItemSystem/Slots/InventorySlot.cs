using Cysharp.Threading.Tasks;
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
        private int _hoverRefreshVersion;

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

            OnItemPlaced(item);

            RefreshHoverState();
            RefreshHoverStateNextFrame().Forget();
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
            RefreshHoverState();
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            ChangeBorderVisibility(false);
            
            if (_tipService != null)
            {
                _tipService.Hide();
            }
        }

        private void RefreshHoverState()
        {
            if (!IsPointerOverSlot())
            {
                ChangeBorderVisibility(false);

                if (_tipService != null)
                {
                    _tipService.Hide();
                }

                return;
            }

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

        private async UniTaskVoid RefreshHoverStateNextFrame()
        {
            var version = ++_hoverRefreshVersion;

            await UniTask.NextFrame();

            if (this == null)
            {
                return;
            }

            if (version != _hoverRefreshVersion)
            {
                return;
            }

            RefreshHoverState();
        }
        
        private bool IsPointerOverSlot()
        {
            var canvas = GetComponentInParent<Canvas>();
            var camera = canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay
                ? canvas.worldCamera
                : null;

            return RectTransformUtility.RectangleContainsScreenPoint(
                (RectTransform)transform,
                Input.mousePosition,
                camera);
        }
    }
}