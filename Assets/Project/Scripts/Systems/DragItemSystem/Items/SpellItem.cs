using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class SpellItem : DragAndDropItemBase
    {
        public SpellBase Spell;

        [SerializeField] private Image _spellImage;

        public SpellSlot CurrentSpellSlot { get; set; }

        public void SetIcon(Sprite sprite)
        {
            if (_spellImage != null)
            {
                _spellImage.sprite = sprite;
            }
        }
    }
}