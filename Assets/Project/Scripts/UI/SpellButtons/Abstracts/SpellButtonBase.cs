using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    public class SpellButtonBase : MonoBehaviour
    {
        public Image Image;

        private void Awake()
        {
            Image = GetComponent<Image>();
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