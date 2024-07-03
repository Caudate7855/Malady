using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.FSM.Player
{
    public class PlayerFsm : MonoBehaviour
    {
        private Fsm _fsm;
        private NavMeshAgent _navMeshAgent;

        private void Start()
        {
            _fsm = new Fsm();
            _navMeshAgent = GetComponent<NavMeshAgent>();

            _fsm.AddState(new FsmStateIdle(_fsm, _navMeshAgent));
            _fsm.AddState(new FsmStateWalk(_fsm, _navMeshAgent));
            
            _fsm.SetState<FsmStateIdle>();
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}