using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class BloodLance : ProjectileSpellBase
    {
        public BloodLance(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            
        }
        
        protected override void DefaultCast()
        {
            SpawnFromOriginForward();
        }
    }
}