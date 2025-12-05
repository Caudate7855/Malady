using Zenject;

namespace Project.Scripts
{
    public class ChurchBoot : LevelBootBase
    {
        [Inject] private DialogueSystemManager _dialogueSystemManager;
        private ChurchController _churchController;
        
        protected override async void Initialize()
        {
            _dialogueSystemManager.Initialize();
            
            _churchController =  await GlobalFactory.CreateAndInitializeAsync<ChurchController>("Church");
        }
    }
}