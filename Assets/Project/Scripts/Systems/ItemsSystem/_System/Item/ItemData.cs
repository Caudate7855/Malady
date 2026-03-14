using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class ItemData
    {
        public ItemType Type { get; }
        public Sprite Sprite { get; }
        public List<StatBase> Stats { get; }
        public ISpellModifier Modifier { get; }
        public GameObject Prefab { get; }

        public ItemData(ItemType type, Sprite sprite, List<StatBase> stats, ISpellModifier modifier, GameObject prefab)
        {
            Type = type;
            Sprite = sprite;
            Stats = stats;
            Modifier = modifier;
            Prefab = prefab;
        }
    }
}