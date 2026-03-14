using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    public abstract class DragAndDropItemBase : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Image SelectionBorder;
        
        public DragAndDropSlot Slot { get; private set; }

        public event Action<DragAndDropItemBase, PointerEventData> BeginDrag;
        public event Action<DragAndDropItemBase, PointerEventData> Drag;
        public event Action<DragAndDropItemBase, PointerEventData> EndDrag;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            ChangeBorderVisibility(false);
        }

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
            var previousSlot = Slot;
            previousSlot?.ChangeBorderVisibility(false);

            BeginDrag?.Invoke(this, eventData);

            ChangeBorderVisibility(true);
        }

        public void OnDrag(PointerEventData eventData)
        {
            Drag?.Invoke(this, eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            EndDrag?.Invoke(this, eventData);
            ChangeBorderVisibility(false);
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