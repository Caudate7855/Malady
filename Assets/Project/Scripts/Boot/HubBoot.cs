using Project.Scripts.Core.Dungeon;
using Zenject;

namespace Project.Scripts
{
    public class HubBoot : LevelBootBase
    {
        [Inject] private HubFactory _hubFactory;
        
        protected override async void Initialize()
        {
            await _hubFactory.Create<HubController>();
        }
    }
}