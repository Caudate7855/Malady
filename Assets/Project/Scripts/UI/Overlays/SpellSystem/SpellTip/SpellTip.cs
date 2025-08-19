using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Overlays
{
    public class SpellTip : MonoBehaviour
    {
        private const float TOP_PADDING = 130f;
        private const float DOWN_PADDING = 150f;
        
        [SerializeField] private TMP_Text _label;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _spellType;
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
            _canvas = FindFirstObjectByType<PanelDispatcher>(FindObjectsInactive.Include).GetComponent<Canvas>();
            Close();
        }

        public void Open()
        {
            gameObject.SetActive(true);

            float textHeight = _description.preferredHeight;
            float newHeight = textHeight + TOP_PADDING + DOWN_PADDING;
            _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, newHeight);
            
            SetPositionToMouse();
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }

        public void SetSpell(SpellSo spellSo)
        {
            _label.text = spellSo.Name;
            _spellType.text = spellSo.Type.ToString();
            
            _description.text = spellSo.Description;
            _image.sprite = spellSo.Icon;

            _resourceTypeImage.sprite = spellSo.SpellCost[0].Resource.Sprite;
            _resourcesCost.text = spellSo.SpellCost[0].Cost.ToString();


            if (spellSo.SpellCost.Count == 2)
            {
                _resourceTypeImage2.sprite = spellSo.SpellCost[1].Resource.Sprite;
                _resourcesCost2.text = spellSo.SpellCost[1].Cost.ToString();
            }
        }

        private void Update()
        {
            SetPositionToMouse();
        }

        private void SetPositionToMouse()
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