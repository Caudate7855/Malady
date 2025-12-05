using UnityEngine;

namespace Project.Scripts
{
    public class SkeletonArcherFsmStateAttack : SummonUnitFsmStateAttack
    {
        private Animator BowAnimator;
        
        public SkeletonArcherFsmStateAttack(Animator animator, Animator bowAnimator, Fsm fsm) : base(animator, fsm)
        {
            BowAnimator = bowAnimator;
        }

        public override void Enter()
        {
            Animator.Play("Attack");
            BowAnimator.Play("Bow_Attack");
        }
    }
}