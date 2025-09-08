using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(menuName = "SO/SpellModificators/SpellModificatorsList", fileName = nameof(SpellModificatorsConfigsContainer))]
    public class SpellModificatorsConfigsContainer : ScriptableObject
    {
        [SerializeField] public List<SpellModificatorConfig> SpellsModificators;

        public SpellModificatorConfig GetModificatorById(string id)
        {
            for (int i = 0, count = SpellsModificators.Count; i < count; i++)
            {
                if (SpellsModificators[i].ID == id)
                {
                    return SpellsModificators[i];
                }
            }

            throw new Exception($"Cannot find spell modificator by id. {id}");
        }
    }
}