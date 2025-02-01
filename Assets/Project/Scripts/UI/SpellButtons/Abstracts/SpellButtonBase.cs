using System;
using System.Collections.Generic;
using Project.Scripts.Overlays;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SpellButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public SpellSo _spell;
        public SpellTip _spellTip;
        
        private Image _image;
        private Button _button;
        
        private SpellTipHandler _spellTipHandler;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
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
        }

        public void SetSpellTipHandler(SpellTipHandler spellTipHandler)
        {
            _spellTipHandler = spellTipHandler;
            _spellTip = _spellTipHandler.GetSpellTip();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowTip();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _spellTip.gameObject.SetActive(false);
        }

        private void ShowTip()
        {
            _spellTip.SetSpell(_spell);
            _spellTip.gameObject.SetActive(true);
        }
    }
}