using System;
using Project.Scripts.Overlays;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Project.Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SpellUIButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler,
        IEndDragHandler, IDragHandler
    {
        public SpellSo Spell;

        [SerializeField] private Sprite _emptySpellSprite;
        [SerializeField] private SpellParentType _spellParentType;
        [SerializeField] private SpellElementType _spellElementType;
        [SerializeField] private SpellList _parentSpellList;
        [SerializeField] private int _index;

        private Image _image;
        private Button _button;
        private RectTransform _rectTransform;

        private SpellTip _spellTip;
        private SpellDragImage _spellDragImage;
        private SpellTipHandler _spellTipHandler;
        private SpellDragImageHandler _spellDragImageHandler;

        private bool _isSetted;
        private bool _isDragging;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();

            ClearSpell();
        }

        public SpellElementType GetSpellElementType()
        {
            return _spellElementType;
        }

        public void Interact()
        {
            _button.Select();
            _button.onClick.Invoke();
        }

        public void SetSpellInfo(SpellSo spellSo)
        {
            Spell = spellSo;
            _image.sprite = spellSo.Icon;

            _isSetted = true;
        }

        public void ClearSpell()
        {
            _isSetted = false;
            _image.sprite = _emptySpellSprite;
            Spell = null;
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
            _spellTip.SetSpell(Spell);
            _spellTip.Open();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_isSetted)
            {
                return;
            }

            _spellTip.Close();

            _spellDragImage.SetSprite(Spell.Icon);
            _spellDragImage.ChangeVisibility(true);
            _spellDragImage.gameObject.transform.position = transform.position;
            _spellDragImage.gameObject.transform.position = Input.mousePosition;

            if (_spellParentType == SpellParentType.MainUi)
            {
                _image.sprite = _emptySpellSprite;
            }

            _isDragging = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isSetted)
            {
                return;
            }

            _spellDragImage.gameObject.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_isSetted)
            {
                return;
            }

            _spellDragImage.ChangeVisibility(false);

            if (_spellDragImage.GetTargetSpellUIButtonBase() != null)
            {
                if (_spellDragImage.GetTargetSpellUIButtonBase()._spellParentType == SpellParentType.MainUi)
                {
                    if (_spellDragImage.GetTargetSpellUIButtonBase()._parentSpellList.SpellsType == Spell.Type)
                    {
                        _spellDragImage.GetTargetSpellUIButtonBase().SetSpellInfo(Spell);
                        _spellDragImage.GetTargetSpellUIButtonBase()._parentSpellList.SetSpell(Spell,
                            _spellDragImage.GetTargetSpellUIButtonBase()._index);
                        
                        if (_parentSpellList != null)
                        {
                            _parentSpellList.RemoveSpell(_index);
                        }
                    }
                }

                if (_spellParentType == SpellParentType.MainUi)
                {
                    Spell = null;
                    ClearSpell();
                }
            }
            else
            {
                _image.sprite = Spell.Icon;
            }

            _isDragging = false;
        }
    }
}