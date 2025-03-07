namespace Project.Scripts
{
    public class HubGameState : GameStateBase
    {
        public HubGameState(Fsm fsm, ISceneLoader sceneLoader) : base(fsm, sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public override void Enter()
        {
            _sceneLoader.LoadScene(GameStateType.Hub);
        }

        public override void Exit()
        {
            
        }
    }
}