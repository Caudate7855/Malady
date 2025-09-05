using UnityEngine;

namespace Project.Scripts.States
{
    public class SummonUnitFsmStateAttack : SummonUnitFsmStateBase
    {
        public SummonUnitFsmStateAttack(Animator animator, Fsm fsm) : base(animator, fsm)
        {
            Animator = animator;
        }
    }
}