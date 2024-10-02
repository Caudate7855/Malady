using UnityEngine.AI;

namespace Project.Scripts.FSM
{
    public class FsmStateBaseIdle : FsmStateBase
    {
        private readonly NavMeshAgent _playerNavMeshAgent;
        
        public FsmStateBaseIdle(Fsm fsm, NavMeshAgent playerNavMeshAgent) : base(fsm)
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
            
            if (_playerNavMeshAgent.velocity.magnitude > 0)
            {
                Fsm.SetState<FsmStateBaseWalk>();
            }
        }
    }
}