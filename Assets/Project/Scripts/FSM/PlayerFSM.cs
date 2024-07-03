using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.FSM
{
    public class PlayerFSM : MonoBehaviour
    {
        private Fsm _fsm;
        private NavMeshAgent _navMeshAgent;

        private FsmStateIdle _fsmStateIdle;
        private FsmStateWalk _fsmStateWalk;
        
        private void Start()
        {
            _fsm = new Fsm();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _fsmStateIdle = new FsmStateIdle(_fsm, _navMeshAgent);
            _fsmStateWalk = new FsmStateWalk(_fsm, _navMeshAgent);
            
            _fsm.AddState(_fsmStateIdle);
            _fsm.AddState(_fsmStateWalk);
            
            _fsm.SetState<FsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}