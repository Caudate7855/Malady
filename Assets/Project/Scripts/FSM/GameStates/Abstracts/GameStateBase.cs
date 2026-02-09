using Zenject;

namespace Project.Scripts
{
    public abstract class GameStateBase : FsmStateBase
    {
        [Inject] protected ISceneLoader _sceneLoader;
        
        public GameStateBase(Fsm fsm, ISceneLoader sceneLoader) : base(fsm)
        {
            
        }
    }
}