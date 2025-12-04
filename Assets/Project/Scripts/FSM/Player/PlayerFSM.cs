using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class PlayerFsm
    {
        public bool IsPossibleToMove = true;
        
        private Fsm _fsm;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private bool _isInitialized;

        public void Initialize(NavMeshAgent navMeshAgent, Animator animator)
        {
            _navMeshAgent = navMeshAgent;
            _animator = animator;
            
            _fsm = new Fsm();
            
            _fsm.AddState(new PlayerFsmStateIdle(_fsm, _navMeshAgent, _animator));
            _fsm.AddState(new PlayerFsmStateRun(_fsm, _navMeshAgent, _animator));
            _fsm.AddState(new PlayerFsmStateCast(_fsm, _animator, this));
            _fsm.AddState(new PlayerFsmStateSummon(_fsm, _animator, this));

            _fsm.SetState<PlayerFsmStateIdle>();
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
    }
}