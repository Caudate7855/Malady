using UnityEngine;

namespace Project.Scripts.States
{
    public class SkeletonArcherFsmStateRun : SummonUnitFsmStateRun
    {
        private Animator BowAnimator;
        
        public SkeletonArcherFsmStateRun(Animator animator, Animator bowAnimator, Fsm fsm) : base(animator, fsm)
        {
            BowAnimator = bowAnimator;
        }

        public override void Enter()
        {
            Animator.Play("Run");
            BowAnimator.Play("Bow_Idle");
        }
    }
}