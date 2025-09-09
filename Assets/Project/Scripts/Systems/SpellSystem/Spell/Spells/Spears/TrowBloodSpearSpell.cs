using JetBrains.Annotations;
using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class TrowBloodSpearSpell : ProjectileSpellBase
    {
        private BloodSpearBonusDamageStat _bloodSpearBonusDamageStat;
        
        public override void Initialize()
        {
            base.Initialize();
            
            _bloodSpearBonusDamageStat = PlayerStats.GetStat<BloodSpearBonusDamageStat>();
            Type = _bloodSpearBonusDamageStat.Type;
            
            ID = "blood_0_1";
            IsInitialized = true;

            //PlayerSpellModificatorsSystem.AddModificator(new BloodSpearModificatorArea());
        }

        public override async void Cast()
        {
            if (PlayerSpellModificatorsSystem.GetModificatorByType<BloodSpearModificatorArea>() != null)
            {
                var projectileCount = 12;
                var angleStep = 360f / projectileCount;

                for (int i = 0; i < projectileCount; i++)
                {
                    var angle = i * angleStep;
                    var rad = Mathf.Deg2Rad * angle;

                    var dir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad));
                    await CastProjectile(PlayerController.Instance.transform.position, dir);
                }
            }
            else
            {
                await CastProjectile(PlayerController.Instance.transform.position, MouseController.GetGroundPosition());
            }
        }

        public override void Clear()
        {
            
        }
    }
}