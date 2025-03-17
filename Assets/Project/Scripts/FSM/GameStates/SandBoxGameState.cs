namespace Project.Scripts
{
    public class SandBoxGameState : GameStateBase
    {
        public SandBoxGameState(Fsm fsm, ISceneLoader sceneLoader) : base(fsm, sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public override void Enter()
        {
            _sceneLoader.LoadScene(GameStateType.SandBox);
        }

        public override void Exit()
        {
            
        }
    }
}