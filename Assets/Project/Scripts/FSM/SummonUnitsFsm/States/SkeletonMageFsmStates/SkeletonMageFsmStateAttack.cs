using UnityEngine;

namespace Project.Scripts.States.SkeletonMageFsmStates
{
    public class SkeletonMageFsmStateAttack : SummonUnitFsmStateAttack
    {
        public SkeletonMageFsmStateAttack(Animator animator, Fsm fsm) : base(animator, fsm)
        {
        }
        
        public override void Enter()
        {
            Animator.CrossFade("Attack", 0.25f);
        }
    }
}