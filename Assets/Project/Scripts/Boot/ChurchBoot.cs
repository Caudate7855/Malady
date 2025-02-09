using Itibsoft.PanelManager;
using Project.Scripts.Core;
using Zenject;

namespace Project.Scripts
{
    public class ChurchBoot : LevelBootBase
    {
        [Inject] private IPanelManager _panelManager;
        
        [Inject] private ChurchFactory _hubFactory;
        [Inject] private DialogueSystemManager _dialogueSystemManager;

        private ChurchController _churchController;
        
        protected override async void Initialize()
        {
            _dialogueSystemManager.Initialize();
            
            _churchController =  await _hubFactory.Create<ChurchController>();
        }
    }
}