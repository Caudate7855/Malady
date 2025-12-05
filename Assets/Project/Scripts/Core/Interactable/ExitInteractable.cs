using Project.Scripts.Abstracts;
using Zenject;

namespace Project.Scripts
{
    public class ExitInteractable : InteractableZoneBase
    {
        [Inject] private Main _main;
        
        public override void Interact()
        {
            _main.ChangeState(SceneType.Church);
            CloseButton();
        }
    }
}