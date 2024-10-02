using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.FSM
{
    public class PlayerFsm : MonoBehaviour
    {
        private Fsm _fsm;
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _fsm = new Fsm();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _fsm.AddState(new FsmStateBaseIdle(_fsm, _navMeshAgent));
            _fsm.AddState(new FsmStateBaseWalk(_fsm, _navMeshAgent));
            
            _fsm.SetState<FsmStateBaseIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}