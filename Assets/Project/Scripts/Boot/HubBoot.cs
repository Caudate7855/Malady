using Itibsoft.PanelManager;
using Project.Scripts.Core;
using Project.Scripts.Core.Dungeon;
using Zenject;

namespace Project.Scripts
{
    public class HubBoot : LevelBootBase
    {
        [Inject] private IPanelManager _panelManager;
        
        [Inject] private HubFactory _hubFactory;
        [Inject] private Blacksmith _blacksmith;
        [Inject] private DialogueFactory _dialogueFactory;
        
        protected override async void Initialize()
        {
            await _hubFactory.Create<HubController>();

            //Instantiate(_blacksmith).PanelManager = _panelManager;

            await _dialogueFactory.Create<Blacksmith>(NpcTypes.Blacksmith);
        }
    }
}