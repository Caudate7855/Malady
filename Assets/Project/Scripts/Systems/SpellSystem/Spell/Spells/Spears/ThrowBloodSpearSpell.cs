using Cysharp.Threading.Tasks;
using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    public class ThrowBloodSpearSpell : ProjectileSpellBase
    {
        private BloodSpearBonusDamageStat _bloodSpearBonusDamageStat;
        
        public override void Initialize()
        {
            base.Initialize();
            
            _bloodSpearBonusDamageStat = PlayerStats.GetStat<BloodSpearBonusDamageStat>();
            Type = _bloodSpearBonusDamageStat.Type;
            
            ID = "blood_0_1";
            IsInitialized = true;
        }

        public override async void Cast()
        {
            if (PlayerSpellModificatorsSystem.GetModificatorByType<BloodSpearModificatorSplit>() != null)
            {
                var projectileCount = 12;
                var angleStep = 360f / projectileCount;

                var startPos = PlayerController.Instance.transform.position;

                for (int i = 0; i < projectileCount; i++)
                {
                    var angle = i * angleStep;
                    var rad = Mathf.Deg2Rad * angle;

                    var dir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)).normalized;

                    await CastProjectile(startPos, startPos + dir, ProjectileType.BloodSpear); 
                }
            }
            else
            {
                await UniTask.Delay(100);
                await CastProjectile(PlayerController.Instance.transform.position, MouseController.GetGroundPosition(), ProjectileType.BloodSpear);
            }
        }
    }
}