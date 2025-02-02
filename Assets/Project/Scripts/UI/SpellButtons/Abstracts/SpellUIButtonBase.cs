using Project.Scripts.Overlays;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SpellUIButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Sprite _emptySpellSprite;
        
        private Image _image;
        private Button _button;
        
        private SpellSo _spell;
        private SpellTip _spellTip;
        private SpellTipHandler _spellTipHandler;
        
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

        public void SetSpellTipHandler(SpellTipHandler spellTipHandler)
        {
            _spellTipHandler = spellTipHandler;
            _spellTip = _spellTipHandler.GetSpellTip();
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
            _savedPosition = transform.position;
            transform.position = Input.mousePosition;
            
            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition; 
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = _savedPosition;
            
            _isDragging = false;
        }
    }
}