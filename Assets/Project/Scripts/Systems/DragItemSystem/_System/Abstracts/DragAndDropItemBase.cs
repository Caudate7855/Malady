using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts
{
    public abstract class DragAndDropItemBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public DragAndDropSlot Slot { get; private set; }

        public event Action<DragAndDropItemBase, PointerEventData> BeginDrag;
        public event Action<DragAndDropItemBase, PointerEventData> Drag;
        public event Action<DragAndDropItemBase, PointerEventData> EndDrag;

        private CanvasGroup _canvasGroup;

        public void SetSlot(DragAndDropSlot slot)
        {
            Slot = slot;
        }

        public void SetBlocksRaycasts(bool value)
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
                if (_canvasGroup == null)
                {
                    _canvasGroup = gameObject.AddComponent<CanvasGroup>();
                }
            }

            _canvasGroup.blocksRaycasts = value;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            BeginDrag?.Invoke(this, eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Drag?.Invoke(this, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag?.Invoke(this, eventData);
        }
    }
}