using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class SpellDragImage : MonoBehaviour
    {
        private Image _image;
        private SpellUIButtonBase _targetSpellUIButtonBase;

        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        public SpellUIButtonBase GetTargetSpellUIButtonBase()
        {
            return _targetSpellUIButtonBase;
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void ChangeVisibility(bool condition)
        {
            gameObject.SetActive(condition);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.GetComponent<SpellUIButtonBase>())
            {
                _targetSpellUIButtonBase = other.GetComponent<SpellUIButtonBase>();
            }
        }
    }
}