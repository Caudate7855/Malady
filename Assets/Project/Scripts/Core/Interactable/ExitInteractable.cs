using Project.Scripts.App;
using Project.Scripts.Core.Abstracts;
using Zenject;

namespace Project.Scripts.Core
{
    public class ExitInteractable : InteractableZoneBase
    {
        [Inject] private Main _main;
        
        public override void Interact()
        {
            _main.ChangeState(GameStateType.Church);
            CloseButton();
        }
    }
}