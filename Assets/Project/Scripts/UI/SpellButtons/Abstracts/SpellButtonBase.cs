using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SpellButtonBase : MonoBehaviour
    {
        public SpellType SpellType;
        
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

        public void UpdateImage(Sprite newSprite)
        {
            _image.sprite = newSprite;
        }

        public void OnMouseEnter()
        {
            ShowSpellTip();
        }

        public void OnMouseExit()
        {
            HideSpellTip();
        }

        private void ShowSpellTip()
        {
                
        }

        private void HideSpellTip()
        {
            
        }
    }
}