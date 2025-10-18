using UnityEngine;

namespace Project.Scripts.States
{
    public class SummonUnitFsmStateRun : SummonUnitFsmStateBase
    {
        public SummonUnitFsmStateRun(Animator animator, Fsm fsm) : base(animator, fsm)
        {
            Animator = animator;
        }

        public override void Enter()
        {
            Debug.Log("Enter - Run");
            Animator.Play("Run");
        }
    }
}