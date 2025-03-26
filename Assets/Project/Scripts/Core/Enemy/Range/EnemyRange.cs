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
            Fsm.AddState(new EnemyRangePatrolState(Fsm, Animator, this));
            
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void Idle()
        {
            if (!CanChangeState)
            {
                return;
            }
            
            _enemyMoveSystem.StopMovement();
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void Move()
        {
            _enemyMoveSystem.ContinueMovement();
            _enemyMoveSystem.FollowTarget(Player.transform);
            Fsm.SetState<EnemyRangeMoveState>();
        }

        public override void Attack()
        {
            if (!CanChangeState)
            {
                return;
            }
            
            _enemyMoveSystem.StopMovement();
            Fsm.SetState<EnemyRangeAttackState>();
        }

        public override void Patrol()
        {
            if (!CanChangeState)
            {
                return;
            }
            
            _enemyMoveSystem.StopMovement();
            Fsm.SetState<EnemyRangePatrolState>();
        }
    }
}