using Project.Scripts.App;
using Project.Scripts.Core.Abstracts;
using Zenject;

namespace Project.Scripts.Core
{
    public class Exit : InteractableZoneBase
    {
        [Inject] private GameDirector _gameDirector;
        
        public override void Interact()
        {
            _gameDirector.ChangeState(GameStateType.Church);
            CloseButton();
        }
    }
}