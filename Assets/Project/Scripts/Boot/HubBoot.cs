using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class HubBoot : LevelBootBase
    {
        [Inject] private DialogueSystemManager _dialogueSystemManager;

        private HubController _hubController;
        
        public override async void Initialize()
        {
            _dialogueSystemManager.Initialize();
            
            _hubController =  await GlobalFactory.CreateAndInitializeAsync<HubController>("Hub");

            GlobalFactory.CreateBook(_hubController.GetBookPosition().transform.position);
            GlobalFactory.CreateExit(_hubController.GetExitPosition().transform.position);
            
            await GlobalFactory.CreateNpcAsync<Undertaker>("Undertaker", _hubController.GetNpcSpawnPosition(NpcTypes.Undertaker));
            await GlobalFactory.CreateNpcAsync<Blacksmith>("Blacksmith", _hubController.GetNpcSpawnPosition(NpcTypes.Blacksmith));
            await GlobalFactory.CreateNpcAsync<Trader>("Trader", _hubController.GetNpcSpawnPosition(NpcTypes.Trader));
            
            await GlobalFactory.CreateCustomCorpseAsync(true,false,false,true, new Vector3(3,0,2));
            await GlobalFactory.CreateDefaultCorpseAsync(new Vector3(3,0,0));
        }
    }
}