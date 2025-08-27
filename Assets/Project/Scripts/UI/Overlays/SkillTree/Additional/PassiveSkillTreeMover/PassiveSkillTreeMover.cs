using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.UI
{
    public class PassiveSkillTreeMover : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private float _minPosY;
        [SerializeField] private float _maxPosY;
        [SerializeField] private float _minPosX;
        [SerializeField] private float _maxPosX;
        
        [SerializeField] private float _moveMultiplier = 10f;
        [SerializeField] private PassiveSkillTreeScaler _scaler; 
        
        private RectTransform _rectTransform;
        private Vector2 _dragOffset;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out var localMousePos
            );

            _dragOffset = _rectTransform.anchoredPosition - localMousePos;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _rectTransform.parent as RectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out var localMousePos))
            {
                Vector2 newPos = localMousePos + _dragOffset;

                float scale = _scaler.GetScale().x;

                float moveFactor = Mathf.Max(0, (scale) * _moveMultiplier);

                float scaledMinX = _minPosX - moveFactor;
                float scaledMaxX = _maxPosX + moveFactor;
                float scaledMinY = _minPosY - moveFactor;
                float scaledMaxY = _maxPosY + moveFactor;

                newPos.x = Mathf.Clamp(newPos.x, scaledMinX, scaledMaxX);
                newPos.y = Mathf.Clamp(newPos.y, scaledMinY, scaledMaxY);

                _rectTransform.anchoredPosition = newPos;
            }
        }
    }
}