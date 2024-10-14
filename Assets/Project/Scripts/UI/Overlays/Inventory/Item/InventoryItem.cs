using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts.Overlays.Inventory
{
    public class InventoryItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image _background;
        [SerializeField] private Image _itemImage;

        [SerializeField] private InventorySlot _inventorySlot;
        
        private void Start()
        {
            
        }

        private void SetBackground()
        {
            
        }

        private void SetItemImage()
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            PutItemInSlot();
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<InventorySlot>() != null)
            {
                _inventorySlot = other.GetComponent<InventorySlot>();
            }
        }

        private void PutItemInSlot()
        {
            if (_inventorySlot != null)
            {
                transform.position = _inventorySlot.transform.position;
            }
        }
    }
}