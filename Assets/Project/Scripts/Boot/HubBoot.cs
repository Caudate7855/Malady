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
        [Inject] private NpcFactory _npcFactory;

        private HubController _hubController;
        
        protected override async void Initialize()
        {
            _hubController =  await _hubFactory.Create<HubController>();

            await _npcFactory.Create<Undertaker>(NpcTypes.Undertaker, _hubController.GetSpawnPosition(NpcTypes.Undertaker));
            await _npcFactory.Create<Blacksmith>(NpcTypes.Blacksmith, _hubController.GetSpawnPosition(NpcTypes.Blacksmith));
            await _npcFactory.Create<Trader>(NpcTypes.Trader, _hubController.GetSpawnPosition(NpcTypes.Trader));
        }
    }
}