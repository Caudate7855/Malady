using System;
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
        public SpellType SpellType;

        private SpellTipHandler _spellTipHandler;
        public SpellTip _spellTip;
        private Button _button;
        private Image _image;

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

        public void SetSpellTipHandler(SpellTipHandler spellTipHandler)
        {
            _spellTipHandler = spellTipHandler;
            _spellTip = _spellTipHandler.GetSpellTip();
        }

        public void UpdateImage(Sprite newSprite)
        {
            _image.sprite = newSprite;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            Debug.Log("POINT ENTER");
            
            _spellTip.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Debug.Log("POINT EXIT");
            
            _spellTip.gameObject.SetActive(false);
        }
    }
}