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
            Animator.CrossFade("Run", 0.25f);
            Debug.Log("Summon unit run");
        }
    }
}