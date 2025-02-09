namespace Project.Scripts
{
    public class ChurchGameState: GameStateBase
    {
        public ChurchGameState(Fsm fsm, ISceneLoader sceneLoader) : base(fsm, sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public override void Enter()
        {
            _sceneLoader.LoadScene(GameStateType.Church);
        }

        public override void Exit()
        {
            
        }
    }
}