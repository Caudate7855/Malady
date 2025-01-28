using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Overlays
{
    public class SpellTip : MonoBehaviour
    {
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _description;
        
        [SerializeField] private Image _image;

        [SerializeField] private Image _resourceTypeImage;
        [SerializeField] private Image _resourceTypeImage2;
        [SerializeField] private TMP_Text _resourcesCost;
        [SerializeField] private TMP_Text _resourcesCost2;
        
        private RectTransform _rectTransform; 
        private Canvas _canvas;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = FindObjectOfType<PanelDispatcher>(true).GetComponent<Canvas>();
            Close();
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            Vector2 mousePosition = Input.mousePosition;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _canvas.transform as RectTransform, 
                mousePosition, 
                _canvas.worldCamera, 
                out var localPoint
            );

            var canvasRect = _canvas.transform as RectTransform;
            var canvasSize = canvasRect.rect.size;
            var objectSize = _rectTransform.rect.size;

            var minX = 0 - canvasSize.x/2;
            var maxX = canvasSize.x/2 - objectSize.x;
            var minY = 0 - canvasSize.y/2;
            var maxY = canvasSize.y/2 - objectSize.y;

            localPoint.x = Mathf.Clamp(localPoint.x, minX, maxX);
            localPoint.y = Mathf.Clamp(localPoint.y, minY, maxY);

            _rectTransform.anchoredPosition = localPoint;
        }
    }
}