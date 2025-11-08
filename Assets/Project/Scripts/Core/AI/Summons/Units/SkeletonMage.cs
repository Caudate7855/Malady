using Project.Scripts.States.SkeletonMageFsmStates;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class SkeletonMage : SummonUnitBase
    {
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
            AiMoveSystem.FollowTarget(PlayerControllerObject.transform);
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