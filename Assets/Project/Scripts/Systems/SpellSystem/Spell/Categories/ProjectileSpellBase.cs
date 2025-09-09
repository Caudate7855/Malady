using Cysharp.Threading.Tasks;
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

        protected virtual async UniTask<ProjectileBase> CastProjectile(Vector3 startPosition, Vector3 targetPosition)
        {
            var direction = (targetPosition - startPosition);
            direction.y = 0; 
            direction.Normalize();

            await UniTask.Delay(100);
            
            var instance = await _globalFactory.CreateAsync<BloodSpear>(BloodSpearAddress, startPosition);

            var pos = instance.transform.position;
            pos.y = 1.45f;
            instance.transform.position = pos;

            if (direction != Vector3.zero)
            {
                instance.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
            }

            instance.Initialize(direction);
            return instance;
        }

        public override void Clear()
        {
            
        }
    }
}