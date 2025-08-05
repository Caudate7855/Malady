using System;
using JetBrains.Annotations;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class MouseFsm : ITickable
    {
        private Fsm _fsm;
        private bool _isInitialized;

        public void Initialize()
        {
            _fsm = new Fsm();

            _fsm.AddState(new DefaultMouseState(_fsm));
            _fsm.AddState(new OverAttackableMouseState(_fsm));
            _fsm.AddState(new OverInteractableMouseState(_fsm));
            _fsm.AddState(new OverUiMouseState(_fsm));

            _fsm.SetState<DefaultMouseState>();
        }

        public void SetState<T>() where T : FsmStateBase
        {
            _fsm.SetState<T>();
        }

        public Type GetCurrentStateType()
        {
            return _fsm.CurrentState.GetType();
        }

        public FsmStateBase GetCurrentState()
        {
            return _fsm.CurrentState;
        }

        public void Tick()
        {
            _fsm.Update();
        }
    }
}