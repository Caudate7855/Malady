using UnityEngine;

namespace Project.Scripts
{
    public class SummonUnitFsmStateAttack : SummonUnitFsmStateBase
    {
        public SummonUnitFsmStateAttack(Animator animator, Fsm fsm) : base(animator, fsm)
        {
            Animator = animator;
        }

        public override void Enter()
        {
            Debug.Log("Enter - Attack");
            Animator.Play("Attack");
        }
    }
}