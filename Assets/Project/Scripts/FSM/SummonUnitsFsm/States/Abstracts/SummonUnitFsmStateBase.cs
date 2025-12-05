using UnityEngine;

namespace Project.Scripts
{
    public abstract class SummonUnitFsmStateBase : FsmStateBase
    {
        protected Animator Animator;
        
        protected SummonUnitFsmStateBase(Animator animator, Fsm fsm) : base(fsm)
        {
            Animator = animator;
        }
    }
}