using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "SpellSo", menuName = "SO/Spells/SpelSo")]
    public class SpellSo : ScriptableObject
    {
        public string Id;
        public SpellType Type;
        public SpellElementType SpellElementType;  
        public string Name;
        public Sprite Icon;
        [TextArea(1,20)] public string Description;
        public List<SpellCost> SpellCost;
    }
}