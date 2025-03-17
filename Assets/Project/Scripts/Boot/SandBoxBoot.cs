using Cysharp.Threading.Tasks;
using Itibsoft.PanelManager;
using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class SandBoxBoot : LevelBootBase
    {
        [Inject] private IPanelManager _panelManager;
        
        [Inject] private SandBoxFactory _sandBoxFactory;
        [Inject] private NpcFactory _npcFactory;
        [Inject] private CorpseFactory _corpseFactory;
        [Inject] private InteractableFactory _interactableFactory;
        [Inject] private DialogueSystemManager _dialogueSystemManager;
        [Inject] private EnemyFactory _enemyFactory;

        private SandBoxController _sandBoxController;
        
        protected override async void Initialize()
        {
            _dialogueSystemManager.Initialize();
            
            _sandBoxController =  await _sandBoxFactory.Create<SandBoxController>();

            _interactableFactory.CreateBook(_sandBoxController.GetBookParentObject());
            _interactableFactory.CreateExit(_sandBoxController.GetExitParentObject());
            
            await _npcFactory.CreateNpcAsync<Undertaker>(NpcTypes.Undertaker, _sandBoxController.GetNpcSpawnPosition(NpcTypes.Undertaker));
            await _npcFactory.CreateNpcAsync<Blacksmith>(NpcTypes.Blacksmith, _sandBoxController.GetNpcSpawnPosition(NpcTypes.Blacksmith));
            await _npcFactory.CreateNpcAsync<Trader>(NpcTypes.Trader, _sandBoxController.GetNpcSpawnPosition(NpcTypes.Trader));
            await _corpseFactory.CreateCustomCorpse(true,false,false,true, new Vector3(3,0,2));
            await _corpseFactory.CreateDefaultCorpse(new Vector3(3,0,0));

            await CreateTestEnemies();
        }
        
        private async UniTask CreateTestEnemies()
        {
            await _enemyFactory.Create<EnemyMelee>(EnemyTypes.Melee, new Vector3(1, 0, 0));
            await _enemyFactory.Create<EnemyRange>(EnemyTypes.Range, new Vector3(2, 0, 0));
        }
    }
}