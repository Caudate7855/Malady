using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class ItemData
    {
        public ItemType ItemType { get; }
        public Sprite Sprite { get; }
        public List<StatBase> Stats { get; }
        public ISpellModifier Modifier { get; }

        public ItemData(ItemType itemType, Sprite sprite, List<StatBase> stats, ISpellModifier modifier)
        {
            ItemType = itemType;
            Sprite = sprite;
            Stats = stats;
            Modifier = modifier;
        }
    }
}