using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class BoneSpear : ProjectileSpellBase
    {
        public BoneSpear(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
        
        }

        protected override void DefaultCast()
        {
            SpawnFromOriginForward();
        }
    }
}