namespace Project.Scripts
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