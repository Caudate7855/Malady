using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Overlays
{
    public class SpellDragImage : MonoBehaviour
    {
        private Image _image;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void ChangeVisibility(bool condition)
        {
            gameObject.SetActive(condition);
        }
    }
}