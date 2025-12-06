using DG.Tweening;
using UnityEngine;

namespace Project.Scripts
{
    public class BloodSpear: ProjectileBase
    {
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