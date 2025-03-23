using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyRangeMoveState : FsmStateBase
    {
        private Animator _animator;
        
        public EnemyRangeMoveState(Fsm fsm, Animator animator) : base(fsm)
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