using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.FSM
{
    public class FsmStateIdle : FsmState
    {
        private NavMeshAgent _playerNavMeshAgent;
        
        public FsmStateIdle(Fsm fsm, NavMeshAgent playerNavMeshAgent) : base(fsm)
        {
            _playerNavMeshAgent = playerNavMeshAgent;
        }

        public override void Enter()
        {
            Debug.Log("IDLE ENTER");
        }
        
        public override void Exit()
        {
            Debug.Log("IDLE EXIT");
        }

        public override void Update()
        {
            Debug.Log("IDLE UPDATE");
            
            if (_playerNavMeshAgent.velocity.magnitude > 0)
            {
                Fsm.SetState<FsmStateWalk>();
            }
        }
    }
}