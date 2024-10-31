using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.FSM
{
    public class PlayerFsm : MonoBehaviour
    {
        private Fsm _fsm;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;

        private void Start()
        {
            _fsm = new Fsm();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponentInChildren<Animator>();

            _fsm.AddState(new PlayerFsmStateIdle(_fsm, _navMeshAgent, _animator));
            _fsm.AddState(new PlayerFsmStateWalk(_fsm, _navMeshAgent, _animator));
            _fsm.AddState(new PlayerFsmStateCast(_fsm, _navMeshAgent, _animator));
            
            _fsm.SetState<PlayerFsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }

        public void SetState<T>() where T : FsmStateBase
        {
            _fsm.SetState<T>();
        }
    }
}