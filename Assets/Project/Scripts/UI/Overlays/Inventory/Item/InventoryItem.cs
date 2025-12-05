using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public SlotType SlotType;

        [SerializeField] private Image _background;
        [SerializeField] private Image _itemImage;
        [SerializeField] private InventorySlot _newInventorySlot;

        public InventorySlot CurrentInventorySlot;

        private GameObject _parentObject;

        public void OnBeginDrag(PointerEventData eventData)
        {
            CurrentInventorySlot.OnDragItem();
            transform.position = Input.mousePosition; 
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition; 
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            PutItemInSlot();
            _newInventorySlot = null;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<InventorySlot>() != null)
            {
                _newInventorySlot = other.GetComponent<InventorySlot>();
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.GetComponent<InventorySlot>() != null)
            {
                if (_newInventorySlot == other.GetComponent<InventorySlot>())
                {
                    _newInventorySlot = null; 
                }
            }
        }

        private void PutItemInSlot()
        {
            if (_newInventorySlot != null && _newInventorySlot.SlotType == SlotType && !_newInventorySlot.IsContainItem
                || _newInventorySlot != null && _newInventorySlot.SlotType == SlotType.Inventory && !_newInventorySlot.IsContainItem)
            {
                CurrentInventorySlot.RemoveItem();
                _newInventorySlot.AddItem(this);
                CurrentInventorySlot = _newInventorySlot;
            }
            else
            {
                SetItem(CurrentInventorySlot.Item);
            }
        }

        private void SetItem(InventoryItem item)
        {
            transform.position = CurrentInventorySlot.transform.position; 
            CurrentInventorySlot.Item = item; 
            CurrentInventorySlot.IsContainItem = true; 
        }
    }
}
