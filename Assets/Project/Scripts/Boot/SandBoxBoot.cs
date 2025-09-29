using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class SandBoxBoot : LevelBootBase
    {
        [SerializeField] private SandBoxController _sandBoxController;
        
        [Inject] private GlobalFactory _globalFactory;

        protected override async void Initialize()
        {
            _globalFactory.CreateBook(_sandBoxController.GetBookPosition().transform.position);
            
            await _globalFactory.CreateCustomCorpseAsync(true,false,false,true, new Vector3(3,0,2));
            await _globalFactory.CreateDefaultCorpseAsync(new Vector3(3,0,0));

            CreateTestEnemies();
        }

        private async void CreateTestEnemies()
        {
            await _globalFactory.CreateEnemyAsync<EnemyRange>("EnemyRange", new Vector3(5,0,0));
        }
    }
}