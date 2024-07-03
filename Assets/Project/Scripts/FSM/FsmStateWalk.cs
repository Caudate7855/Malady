using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.FSM
{
    public class FsmStateWalk : FsmState
    {
        private NavMeshAgent _playerNavMeshAgent;
        
        public FsmStateWalk(Fsm fsm, NavMeshAgent playerNavMeshAgent) : base(fsm)
        {
            _playerNavMeshAgent = playerNavMeshAgent;
        }
        
        public override void Enter()
        {
            Debug.Log("WALK ENTER");
        }
        
        public override void Exit()
        {
            Debug.Log("WALK EXIT");
        }

        public override void Update()
        {
            Debug.Log("WALK UPDATE");
            
            if (_playerNavMeshAgent.velocity.magnitude == 0)
            {
                Fsm.SetState<FsmStateIdle>();
            }
        }
    }
}