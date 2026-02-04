using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "SpellsContainer", menuName = "SO/Spells/SpellsContainer")]
    public class SpellsContainerSo : ScriptableObject
    {
        [SerializeField] private List<SpellSo> Spells;
        
        public SpellSo GetSpell(SpellElementType type, int row, int column)
        {
            var typeString = type.ToString().ToLower();
            
            var spellId = $"{typeString}_{row}_{column}";
            
            for (int i = 0, count = Spells.Count; i < count; i++)
            {
                if (Spells[i].Id == spellId)
                {
                    return Spells[i];
                }
            }

            throw new Exception($"SpellSystem: Cannot find spell with id {spellId}");
        }
    }
}