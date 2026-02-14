using Cysharp.Threading.Tasks;
using Project.Scripts.Modifs;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class SandBoxBoot : LevelBootBase
    {
        [Inject] private SpellSystem _spellSystem;
        
        [SerializeField] private SandBoxController _sandBoxController;
        
        public override async void Initialize()
        {
            base.Initialize();
            
            _sandBoxController.Initialize();
            
            GlobalFactory.CreateBook(_sandBoxController.GetBookPosition().transform.position);
            
            await GlobalFactory.CreateCustomCorpseAsync(true,false,false,true, new Vector3(3,0,2));
            await GlobalFactory.CreateDefaultCorpseAsync(new Vector3(3,0,0));

            CreateTestEnemies();
            BuildNavMeshAndSpawnAsync().Forget(Debug.LogException);
        }

        private async void CreateTestEnemies()
        {
            await GlobalFactory.CreateEnemyAsync<EnemyRange>("EnemyRange", new Vector3(5,0,0));
        }

        [Button]
        public void AddModifs()
        {
            _spellSystem.AddFor<BloodLance, TripleShotModifier>();
            _spellSystem.AddFor<BloodLance, SinMoveModifier>();
        }
        
        [Button]
        public void RemoveModifs()
        {
            _spellSystem.RemoveFor<BloodLance, TripleShotModifier>();
            _spellSystem.RemoveFor<BloodLance, SinMoveModifier>();
        }
    }
}