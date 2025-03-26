using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyRange : EnemyBase
    {
        protected override void InitializeFsm()
        {
            Fsm.AddState(new EnemyRangeIdleState(Fsm, Animator, this));
            Fsm.AddState(new EnemyRangeMoveState(Fsm, Animator, this));
            Fsm.AddState(new EnemyRangeAttackState(Fsm, Animator, this));
            
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void Idle()
        {
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void Move()
        {
            Fsm.SetState<EnemyRangeMoveState>();
        }

        public override void Attack()
        {
            Fsm.SetState<EnemyRangeAttackState>();
        }
    }
}