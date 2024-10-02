using Zenject;

namespace Project.Scripts.FSM
{
    public class GameStateBase : FsmStateBase
    {
        [Inject] protected ISceneLoader _sceneLoader;
        
        public GameStateBase(Fsm fsm, ISceneLoader sceneLoader) : base(fsm)
        {
            
        }
    }
}