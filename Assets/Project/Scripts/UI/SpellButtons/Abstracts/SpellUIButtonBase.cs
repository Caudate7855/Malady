using System;
using Project.Scripts.Overlays;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SpellUIButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Sprite _defaultSprite;
        
        private Image _image;
        private Button _button;
        
        private SpellSo _spell;
        private SpellTip _spellTip;
        private SpellTipHandler _spellTipHandler;
        
        private bool _isSetted;

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
            _image.sprite = _defaultSprite;
        }

        public void SetSpellTipHandler(SpellTipHandler spellTipHandler)
        {
            _spellTipHandler = spellTipHandler;
            _spellTip = _spellTipHandler.GetSpellTip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!_isSetted)
            {
                return;
            }
            
            ShowTip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _spellTip.Close();
        }

        private void ShowTip()
        {
            _spellTip.SetSpell(_spell);
            _spellTip.Open();
        }
    }
}