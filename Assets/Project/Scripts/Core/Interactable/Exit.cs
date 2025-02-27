using Project.Scripts.App;
using Project.Scripts.Core.Abstracts;
using Zenject;

namespace Project.Scripts.Core
{
    public class Exit : InteractableZoneBase
    {
        [Inject] private Main _main;
        
        public override void Interact()
        {
            _main.ChangeState(GameStateType.Church);
            CloseButton();
        }
    }
}