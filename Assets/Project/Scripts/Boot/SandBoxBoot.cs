using UnityEngine;

namespace Project.Scripts
{
    public class SandBoxBoot : LevelBootBase
    {
        [SerializeField] private SandBoxController _sandBoxController;

        public override async void Initialize()
        {
            _sandBoxController.Initialize();
            
            GlobalFactory.CreateBook(_sandBoxController.GetBookPosition().transform.position);
            
            await GlobalFactory.CreateCustomCorpseAsync(true,false,false,true, new Vector3(3,0,2));
            await GlobalFactory.CreateDefaultCorpseAsync(new Vector3(3,0,0));

            CreateTestEnemies();
        }

        private async void CreateTestEnemies()
        {
            await GlobalFactory.CreateEnemyAsync<EnemyRange>("EnemyRange", new Vector3(5,0,0));
        }
    }
}