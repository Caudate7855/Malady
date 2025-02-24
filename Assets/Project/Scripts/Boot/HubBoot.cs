using Itibsoft.PanelManager;
using Project.Scripts.Core;
using Project.Scripts.Core.Hub;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class HubBoot : LevelBootBase
    {
        [Inject] private IPanelManager _panelManager;
        
        [Inject] private HubFactory _hubFactory;
        [Inject] private NpcFactory _npcFactory;
        [Inject] private CorpseFactory _corpseFactory;
        [Inject] private InteractableFactory _interactableFactory;
        [Inject] private DialogueSystemManager _dialogueSystemManager;

        private HubController _hubController;
        
        protected override async void Initialize()
        {
            _dialogueSystemManager.Initialize();
            
            _hubController =  await _hubFactory.Create<HubController>();

            _interactableFactory.CreateBook(_hubController.GetBookParentObject());
            _interactableFactory.CreateExit(_hubController.GetExitParentObject());
            
            await _npcFactory.CreateNpcAsync<Undertaker>(NpcTypes.Undertaker, _hubController.GetNpcSpawnPosition(NpcTypes.Undertaker));
            await _npcFactory.CreateNpcAsync<Blacksmith>(NpcTypes.Blacksmith, _hubController.GetNpcSpawnPosition(NpcTypes.Blacksmith));
            await _npcFactory.CreateNpcAsync<Trader>(NpcTypes.Trader, _hubController.GetNpcSpawnPosition(NpcTypes.Trader));
            await _corpseFactory.CreateCustomCorpse(true,false,false,true, new Vector3(3,0,2));
        }
    }
}