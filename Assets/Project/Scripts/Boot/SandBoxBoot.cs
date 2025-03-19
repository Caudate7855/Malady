using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class SandBoxBoot : LevelBootBase
    {
        [SerializeField] private SandBoxController _sandBoxController;
        
        [Inject] private CorpseFactory _corpseFactory;
        [Inject] private EnemyFactory _enemyFactory;

        protected override async void Initialize()
        {
            await _corpseFactory.CreateCustomCorpse(true,false,false,true, new Vector3(3,0,2));
            await _corpseFactory.CreateDefaultCorpse(new Vector3(3,0,0));

            CreateTestEnemies();
        }

        private async void CreateTestEnemies()
        {
            await _enemyFactory.Create<EnemyRange>(EnemyTypes.Range, new Vector3(5,0,0));
        }
    }
}