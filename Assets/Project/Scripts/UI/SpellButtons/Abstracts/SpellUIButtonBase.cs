using Project.Scripts.Overlays;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SpellUIButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler,
        IEndDragHandler, IDragHandler
    {
        [SerializeField] private Sprite _emptySpellSprite;
        [SerializeField] private SpellParentType _spellParentType;
        
        private Image _image;
        private Button _button;
        private RectTransform _rectTransform;

        private SpellSo _spell;
        private SpellTip _spellTip;
        private SpellDragImage _spellDragImage;
        private SpellTipHandler _spellTipHandler;
        private SpellDragImageHandler _spellDragImageHandler;

        private bool _isSetted;

        private Vector2 _savedPosition;
        private bool _isDragging;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();

            ClearSpell();
        }

        public void Interact()
        {
            _button.Select();
            _button.onClick.Invoke();
        }

        public void SetSpellInfo(SpellSo spellSo)
        {
            _spell = spellSo;
            _image.sprite = spellSo.Icon;

            _isSetted = true;
        }

        public void ClearSpell()
        {
            _isSetted = false;
            _image.sprite = _emptySpellSprite;
        }

        public void SetSpellHandlers(SpellTipHandler spellTipHandler, SpellDragImageHandler spellDragImageHandler)
        {
            _spellTipHandler = spellTipHandler;
            _spellTip = _spellTipHandler.GetSpellTip();

            _spellDragImageHandler = spellDragImageHandler;
            _spellDragImage = _spellDragImageHandler.GetSpellDragImage();
            _spellDragImage.ChangeVisibility(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isSetted || _isDragging)
            {
                return;
            }

            ShowTip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (!_isSetted)
            {
                return;
            }

            _spellTip.Close();
        }

        private void ShowTip()
        {
            _spellTip.SetSpell(_spell);
            _spellTip.Open();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _spellTip.Close();

            if (_spellParentType == SpellParentType.Book)
            {
                _spellDragImage.SetSprite(_spell.Icon);
                _spellDragImage.ChangeVisibility(true);
                _spellDragImage.gameObject.transform.position = transform.position;
                _spellDragImage.gameObject.transform.position = Input.mousePosition;
            }
            else
            {
                _savedPosition = transform.position;
                transform.position = Input.mousePosition;
            }

            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_spellParentType == SpellParentType.Book)
            {
                _spellDragImage.gameObject.transform.position = Input.mousePosition;
            }
            else
            {
                transform.position = Input.mousePosition;
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_spellParentType == SpellParentType.Book)
            {
                _spellDragImage.gameObject.transform.position = _savedPosition;
                _spellDragImage.ChangeVisibility(false);
            }
            else
            {
                transform.position = _savedPosition;
            }

            _isDragging = false;
        }
    }
}