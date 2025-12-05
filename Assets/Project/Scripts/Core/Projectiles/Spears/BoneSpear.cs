using UnityEngine;

namespace Project.Scripts
{
    public class BoneSpear : ProjectileBase
    {
        public override float Speed { get; protected set; } = 20f;

        public override void Initialize(Vector3 direction, EnemyBase target = default)
        {
            base.Initialize(direction, target);
        }

        protected override float CalculateDamage()
        {
            var damage = PlayerStats.GetStat<BoneSpearBonusDamageStat>().Value;
            
            return damage;
        }
    }
}