using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts.Spells
{
    [Serializable]
    public sealed class BloodAbsorption : SpellBase
    {
        public BloodAbsorption(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            
        }
    }
}