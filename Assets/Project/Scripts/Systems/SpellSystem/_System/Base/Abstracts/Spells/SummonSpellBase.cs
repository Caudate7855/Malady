using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public abstract class SummonSpellBase : ProjectileSpellBase
    {
        public override SpellType SpellType { get; set; } = SpellType.Summon;
        
        public int Count { get; private set; } = 1;

        public Vector3 SummonPositionWorld { get; private set; }
        public bool HasSummonPosition { get; private set; }

        protected SummonSpellBase(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            Speed = 0f;
        }

        public void SetSummon(Vector3 positionWorld, int count)
        {
            SummonPositionWorld = positionWorld;
            HasSummonPosition = true;
            Count = Mathf.Max(1, count);
        }

        protected override void DefaultCast()
        {
            var origin = Origin ?? throw new Exception("Origin is null");

            var pos = HasSummonPosition ? SummonPositionWorld : origin.position;
            var dir = origin.forward;

            var n = Mathf.Max(1, Count);
            for (var k = 0; k < n; k++)
                SpawnInternal(pos, dir);
        }
    }
}