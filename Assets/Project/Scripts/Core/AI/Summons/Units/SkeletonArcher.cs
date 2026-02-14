using UnityEngine;
using Zenject;

namespace Project.Scripts.Summons
{
    public class SkeletonArcher : SummonUnitBase
    {
        [SerializeField] private Animator BowAnimator;
        
        public override void Initialize()
        {
            Fsm.AddState(new SkeletonArcherFsmStateIdle(Animator, BowAnimator, Fsm));
            Fsm.AddState(new SkeletonArcherFsmStateAttack(Animator, BowAnimator, Fsm));
            Fsm.AddState(new SkeletonArcherFsmStateRun(Animator, BowAnimator, Fsm));
            
            Fsm.SetState<SkeletonArcherFsmStateIdle>();
        }
        
        public override void Attack()
        {
            AiMoveSystem.StopMovement();
            Fsm.SetState<SkeletonArcherFsmStateAttack>();
        }

        public override void Idle()
        {
            AiMoveSystem.StopMovement();
            Fsm.SetState<SkeletonArcherFsmStateIdle>();
        }

        public override void MoveToPlayer()
        {
            AiMoveSystem.ContinueMovement();
            AiMoveSystem.FollowTarget(PlayerController.transform);
            Fsm.SetState<SkeletonArcherFsmStateRun>();
        }
        
        public override void MoveTo(Transform targetTransform)
        {
            AiMoveSystem.ContinueMovement();
            AiMoveSystem.FollowTarget(targetTransform);
            Fsm.SetState<SkeletonArcherFsmStateRun>();
        }
    }
}