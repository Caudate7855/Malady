using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public sealed class InventoryItem : DragAndDropItemBase
    {
        public SlotType SlotType;

        [SerializeField] private Image _background;
        [SerializeField] private Image _itemImage;

        public InventorySlot CurrentInventorySlot { get; set; }

        public void SetBackground(Sprite sprite)
        {
            if (_background != null)
            {
                _background.sprite = sprite;
            }
        }

        public void SetIcon(Sprite sprite)
        {
            if (_itemImage != null)
            {
                _itemImage.sprite = sprite;
            }
        }
    }
}