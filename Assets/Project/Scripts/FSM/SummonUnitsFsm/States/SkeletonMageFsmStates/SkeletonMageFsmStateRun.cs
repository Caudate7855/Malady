using UnityEngine;

namespace Project.Scripts.States.SkeletonMageFsmStates
{
    public class SkeletonMageFsmStateRun : SummonUnitFsmStateBase
    {
        public SkeletonMageFsmStateRun(Animator animator, Fsm fsm) : base(animator, fsm)
        {
        }
        
        public override void Enter()
        {
            Animator.Play("Run");
        }
    }
}