using UnityEngine;

namespace Project.Scripts
{
    public class SkeletonArcherFsmStateIdle : SummonUnitFsmStateIdle
    {
        private Animator BowAnimator;
        
        public SkeletonArcherFsmStateIdle(Animator animator, Animator bowAnimator, Fsm fsm) : base(animator, fsm)
        {
            BowAnimator = bowAnimator;
        }

        public override void Enter()
        {
            Animator.Play("Idle");
            BowAnimator.Play("Bow_Idle");
        }
    }
}