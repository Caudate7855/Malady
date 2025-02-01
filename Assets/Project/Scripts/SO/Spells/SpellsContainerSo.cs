using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "SpellsContainer", menuName = "SO/Spells/SpellsContainer")]
    public class SpellsContainerSo : ScriptableObject
    {
        [SerializeField] private List<SpellSo> Spells;
        
        public SpellSo GetSpell(string id)
        {
            for (int i = 0, count = Spells.Count; i < count; i++)
            {
                if (Spells[i].Id == id)
                {
                    return Spells[i];
                }
            }

            throw new Exception($"SpellSystem: Cannot find spell with id {id}");
        }
    }
}