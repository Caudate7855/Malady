using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SpellButtonBase : MonoBehaviour
    {
        public SpellType SpellType;
        public SpellBaseType SpellBaseType;
        
        public Button Button;
        public Image Image;

        private void Awake()
        {
            Button = GetComponent<Button>();
            Image = GetComponent<Image>();
        }

        public void Interact()
        {
            Button.onClick.Invoke();
        }
        
        private void UpdateImage()
        {
            
        }

        private void UpdateSpell()
        {
            UpdateImage();
        }
    }
}