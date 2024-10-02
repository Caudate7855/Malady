using System;
using System.Collections.Generic;

namespace Project.Scripts.FSM
{
    public class Fsm
    {
        private FsmStateBase _stateBaseCurrent;
        private Dictionary<Type, FsmStateBase> _states = new();

        public void AddState(FsmStateBase stateBase)
        {
            _states.Add(stateBase.GetType(), stateBase);
        }

        public void SetState<T>() where T : FsmStateBase
        {
            var type = typeof(T);

            if (_stateBaseCurrent?.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newState))
            {
                _stateBaseCurrent?.Exit();

                _stateBaseCurrent = newState;
                
                _stateBaseCurrent.Enter();
            }
        }

        public void Update()
        {
            _stateBaseCurrent?.Update();
        }
    }
}