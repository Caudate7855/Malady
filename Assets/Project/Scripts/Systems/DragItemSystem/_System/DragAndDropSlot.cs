using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class DragAndDropSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private DragAndDropSlotKind _kind;
        [SerializeField] private RectTransform _content;
        [SerializeField] private Image SelectionBorder;
        
        private DragAndDropItemBase _item;

        public DragAndDropSlotKind Kind => _kind;
        public DragAndDropItemBase Item => _item;
        public bool HasItem => _item != null;

        public event Action<DragAndDropSlot, PointerEventData> Dropped;

        public void SetItem(DragAndDropItemBase item)
        {
            _item = item;

            if (_item != null)
            {
                _item.SetSlot(this);
                _item.transform.SetParent(_content != null ? _content : transform, false);
            }
        }

        public DragAndDropItemBase ClearItem()
        {
            var item = _item;
            _item = null;
            return item;
        }

        public void OnDrop(PointerEventData eventData)
        {
            Dropped?.Invoke(this, eventData);
        }
        
        public void ChangeBorderVisibility(bool condition)
        {
            if (SelectionBorder != null)
            {
                if (condition)
                {
                    SelectionBorder.transform.SetAsLastSibling();
                    SelectionBorder.gameObject.SetActive(true);
                }
                else
                {
                    SelectionBorder.gameObject.SetActive(false);
                }
            }
        }
    }
}