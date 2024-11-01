using System;
using System.Collections.Generic;

namespace Project.Scripts.FSM
{
    public class Fsm
    {
        public FsmStateBase CurrentState;
        private Dictionary<Type, FsmStateBase> _states = new();

        public void AddState(FsmStateBase stateBase)
        {
            _states.Add(stateBase.GetType(), stateBase);
        }

        public void SetState<T>() where T : FsmStateBase
        {
            var type = typeof(T);

            if (CurrentState?.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newState))
            {
                CurrentState?.Exit();

                CurrentState = newState;
                
                CurrentState.Enter();
            }
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }
}