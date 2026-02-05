using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts.Spells
{
    [Serializable]
    public sealed class BloodLance : SpellBase
    {
        public BloodLance(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
        
        }
    }
}