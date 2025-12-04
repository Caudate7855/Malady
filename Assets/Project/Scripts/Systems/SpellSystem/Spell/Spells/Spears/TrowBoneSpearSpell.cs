using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
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

            PlayerSpellModificatorsSystem.AddModificator(new BoneSpearModificatorArea());
        }

        public override async void Cast()
        {
            if (PlayerSpellModificatorsSystem.GetModificatorByType<BoneSpearModificatorArea>() != null)
            {
                var projectileCount = 12;
                var angleStep = 360f / projectileCount;

                var startPos = PlayerController.transform.position;

                for (int i = 0; i < projectileCount; i++)
                {
                    var angle = i * angleStep;
                    var rad = Mathf.Deg2Rad * angle;

                    var dir = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)).normalized;

                    await CastProjectile(startPos, startPos + dir, ProjectileType.BoneSpear); 
                }
            }
            else
            {
                await UniTask.Delay(100);
                await CastProjectile(PlayerController.transform.position, MouseController.GetGroundPosition(), ProjectileType.BoneSpear);
            }
        }

        public override void Clear()
        {
            
        }
    }
}