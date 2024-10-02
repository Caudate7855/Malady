using Project.Scripts.App;
using Zenject;

namespace Project.Scripts.FSM
{
    public class MainMenuGameStateBase : GameStateBase
    {
        public MainMenuGameStateBase(Fsm fsm, ISceneLoader sceneLoader) : base(fsm, sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public override void Enter()
        {
            _sceneLoader.LoadScene(GameStateType.MainMenu);
        }
    }
}