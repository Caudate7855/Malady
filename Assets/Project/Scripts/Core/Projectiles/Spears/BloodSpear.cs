using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core
{
    public class BloodSpear: ProjectileBase
    {
        public override float Speed { get; protected set; } = 20f;

        public override void Initialize(Vector3 direction, EnemyBase target = default)
        {
            base.Initialize(direction, target);
        }

        private void Start()
        {
            View.transform
                .DOLocalRotate(new Vector3(360, 0, 0), 0.5f, RotateMode.LocalAxisAdd)
                .SetLoops(-1)
                .SetEase(Ease.Linear);
        }

        protected override float CalculateDamage()
        {
            var damage = PlayerStats.GetStat<BloodSpearBonusDamageStat>().Value;
            
            return damage;
        }
    }
}