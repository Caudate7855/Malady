using UnityEngine.AI;

namespace Project.Scripts.FSM
{
    public class FsmStateWalk : FsmState
    {
        private readonly NavMeshAgent _playerNavMeshAgent;
        
        public FsmStateWalk(Fsm fsm, NavMeshAgent playerNavMeshAgent) : base(fsm)
        {
            _playerNavMeshAgent = playerNavMeshAgent;
        }
        
        public override void Enter()
        {
            
        }
        
        public override void Exit()
        {
            
        }

        public override void Update()
        {
            if (_playerNavMeshAgent.velocity.magnitude == 0)
            {
                Fsm.SetState<FsmStateIdle>();
            }
        }
    }
}