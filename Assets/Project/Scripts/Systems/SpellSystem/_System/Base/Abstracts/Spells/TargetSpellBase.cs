using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public abstract class TargetSpellBase : ProjectileSpellBase
    {
        public override SpellType SpellType { get; set; } = SpellType.Target;
        
        public Transform Target { get; private set; }

        public Vector3 TargetPointWorld { get; private set; }
        public bool HasTargetPoint { get; private set; }

        protected TargetSpellBase(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            Speed = 0f;
        }

        public void SetTarget(Transform target)
        {
            Target = target;
            HasTargetPoint = false;
        }

        public void SetTargetPoint(Vector3 pointWorld)
        {
            Target = null;
            TargetPointWorld = pointWorld;
            HasTargetPoint = true;
        }

        protected override void DefaultCast()
        {
            var origin = Origin ?? throw new Exception("Origin is null");

            var hasTarget = Target != null;
            var pos = hasTarget ? Target.position : (HasTargetPoint ? TargetPointWorld : origin.position);

            var i = SpawnInternal(pos, origin.forward);

            if (!hasTarget) return;

            var go = GetInstance(i);
            if (go == null) return;

            go.transform.SetParent(Target, true);
        }
    }
}