using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class TrowBoneSpearSpell : ProjectileSpellBase
    {
        private BoneSpearBonusDamageStat _boneSpearBonusDamageStat;
        
        public override void Initialize()
        {
            base.Initialize();
            
            _boneSpearBonusDamageStat = PlayerStats.GetStat<BoneSpearBonusDamageStat>();
            Type = _boneSpearBonusDamageStat.Type;
            
            ID = "bones_1_1";
            IsInitialized = true;

            PlayerSpellModificatorsSystem.AddModificator(new BloodSpearModificatorArea());
        }

        public override async void Cast()
        {
            if (PlayerSpellModificatorsSystem.GetModificatorByType<BloodSpearModificatorArea>() != null)
            {
                var projectileCount = 12;
                var angleStep = 360f / projectileCount;

                var startPos = PlayerController.Instance.transform.position;

                for (int i = 0; i < projectileCount; i++)
                {
                    var angle = i * angleStep;
                    var rad = Mathf.Deg2Rad * angle;

                    var dir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)).normalized;

                    await CastProjectile(startPos, startPos + dir); 
                }
            }
            else
            {
                await UniTask.Delay(100);
                await CastProjectile(PlayerController.Instance.transform.position, MouseController.GetGroundPosition());
            }
        }

        public override void Clear()
        {
            
        }
    }
}