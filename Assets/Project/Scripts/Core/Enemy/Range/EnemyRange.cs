using UnityEngine;

namespace Project.Scripts
{
    public class EnemyRange : EnemyBase
    {
        protected override void InitializeFsm()
        {
            Fsm.AddState(new EnemyRangeIdleState(Fsm, Animator, this));
            Fsm.AddState(new EnemyRangeMoveState(Fsm, Animator, this));
            Fsm.AddState(new EnemyRangeAttackState(Fsm, Animator, this));
            Fsm.AddState(new EnemyRangePatrolState(Fsm, Animator, this));
            
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void Idle()
        {
            if (!CanChangeState)
            {
                return;
            }
            
            AiMoveSystem.StopMovement();
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void MoveTo(Transform targetTransform)
        {
            AiMoveSystem.ContinueMovement();
            AiMoveSystem.FollowTarget(targetTransform);
            Fsm.SetState<EnemyRangeMoveState>();
        }

        public override void Attack()
        {
            if (!CanChangeState)
            {
                return;
            }
            
            AiMoveSystem.StopMovement();
            Fsm.SetState<EnemyRangeAttackState>();
        }

        public override void Patrol()
        {
            if (!CanChangeState)
            {
                return;
            }
            
            AiMoveSystem.StopMovement();
            Fsm.SetState<EnemyRangePatrolState>();
        }
    }
}