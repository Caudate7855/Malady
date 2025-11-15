using System;
using Cysharp.Threading.Tasks;
using NUnit.Framework.Constraints;
using Project.Scripts.Core;
using Project.Scripts.UI.Inventory;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class ProjectileSpellBase : SpellBase
    {
        [Inject] private GlobalFactory _globalFactory;
        
        private const string BloodSpearAddress = "BloodSpear";
        private const string BoneSpearAddress = "BoneSpear";
        
        public override void Initialize()
        {
            InventoryController = PanelManager.LoadPanel<InventoryController>();
        }

        public override void Cast()
        {
            
        }

        protected virtual async UniTask<ProjectileBase> CastProjectile(Vector3 startPosition, Vector3 targetPosition, ProjectileType  projectileType)
        {
            var direction = (targetPosition - startPosition);
            direction.y = 0; 
            direction.Normalize();

            ProjectileBase projectile = null;
            
            switch (projectileType)
            {
                case  ProjectileType.BloodSpear:
                    projectile = await _globalFactory.CreateAsync<BloodSpear>(BloodSpearAddress, startPosition);
                    break;
                
                case  ProjectileType.BoneSpear:
                    projectile = await _globalFactory.CreateAsync<BoneSpear>(BoneSpearAddress, startPosition);
                    break;
            }

            if (projectile == null)
            {
                throw new Exception("Cannot cast projectile");
            }
            
            var pos = projectile.transform.position;
            pos.y = 1.45f;
            projectile.transform.position = pos;

            if (direction != Vector3.zero)
            {
                projectile.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }

            projectile.Initialize(direction);
            return projectile;
        }

        public override void Clear()
        {
            
        }
    }
}