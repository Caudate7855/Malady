using Project.Scripts.States;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class SkeletonArcher : SummonUnitBase
    {
        [SerializeField] private Animator BowAnimator;
        
        public override void Initialize()
        {
            Fsm.AddState(new SkeletonArcherFsmStateIdle(Animator, BowAnimator, Fsm));
            Fsm.AddState(new SkeletonArcherFsmStateAttack(Animator, BowAnimator, Fsm));
            Fsm.AddState(new SkeletonArcherFsmStateRun(Animator, BowAnimator, Fsm));
            
            Fsm.SetState<SkeletonArcherFsmStateIdle>();
        }
    }
}