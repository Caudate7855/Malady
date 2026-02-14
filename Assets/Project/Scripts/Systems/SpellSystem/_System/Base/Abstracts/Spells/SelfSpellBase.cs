using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public abstract class SelfSpellBase : ProjectileSpellBase
    {
        public override SpellType SpellType { get; set; } = SpellType.Self;
        
        protected SelfSpellBase(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            Speed = 0f;
        }

        protected override void DefaultCast()
        {
            var origin = Origin ?? throw new Exception("Origin is null");

            var i = SpawnInternal(origin.position, origin.forward);

            var go = GetInstance(i);
            if (go == null) return;

            go.transform.SetParent(origin, true);
        }
    }
}