using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyRangeIdleState : FsmStateBase
    {
        private Animator _animator;
        
        public EnemyRangeIdleState(Fsm fsm, Animator animator) : base(fsm)
        {
            _animator = animator;
        }

        public override void Enter()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}