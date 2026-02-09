using Project.Scripts.Configs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    public sealed class SpellItem : DragAndDropItemBase, IPointerEnterHandler, IPointerExitHandler
    {
        public SpellBase Spell;
        public SpellConfig SpellConfig;

        [SerializeField] private Image _spellImage;

        public SpellSlot CurrentSpellSlot { get; set; }

        public void SetIcon(Sprite sprite)
        {
            if (_spellImage != null)
            {
                _spellImage.sprite = sprite;
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            CurrentSpellSlot?.OnItemPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CurrentSpellSlot?.OnItemPointerExit();
        }
    }
}