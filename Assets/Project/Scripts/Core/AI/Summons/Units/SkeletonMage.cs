using UnityEngine;
using Zenject;

namespace Project.Scripts.Summons
{
    public class SkeletonMage : SummonUnitBase
    {
        [Inject] public PlayerStats PlayerStats;
        [Inject] protected PlayerController PlayerController;
        
        public override void Initialize()
        {
            Fsm.AddState(new SkeletonMageFsmStateIdle(Animator, Fsm));
            Fsm.AddState(new SkeletonMageFsmStateRun(Animator, Fsm));
            Fsm.AddState(new SkeletonMageFsmStateAttack(Animator, Fsm));
            
            Fsm.SetState<SkeletonMageFsmStateIdle>();
        }
        
        public override void Attack()
        {
            AiMoveSystem.StopMovement();
            Fsm.SetState<SkeletonMageFsmStateAttack>();
        }

        public override void Idle()
        {
            AiMoveSystem.StopMovement();
            Fsm.SetState<SkeletonMageFsmStateIdle>();
        }

        public override void MoveToPlayer()
        {
            AiMoveSystem.ContinueMovement();
            AiMoveSystem.FollowTarget(PlayerController.transform);
            Fsm.SetState<SkeletonMageFsmStateRun>();
        }
        
        public override void MoveTo(Transform targetTransform)
        {
            AiMoveSystem.ContinueMovement();
            AiMoveSystem.FollowTarget(targetTransform);
            Fsm.SetState<SkeletonMageFsmStateRun>();
        }
    }
}