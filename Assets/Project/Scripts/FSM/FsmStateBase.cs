namespace Project.Scripts.FSM
{
    public abstract class FsmStateBase
    {
        protected readonly Fsm Fsm;
        protected FsmStateBase CurrentState;

        protected FsmStateBase(Fsm fsm)
        {
            Fsm = fsm;
        }

        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}