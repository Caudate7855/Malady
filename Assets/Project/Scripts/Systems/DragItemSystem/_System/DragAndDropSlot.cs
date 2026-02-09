using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts
{
    public class DragAndDropSlot : MonoBehaviour, IDropHandler
    {
        [SerializeField] private DragAndDropSlotKind _kind;
        [SerializeField] private RectTransform _content;

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
    }
}