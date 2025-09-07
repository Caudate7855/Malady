using Project.Scripts.States.SkeletonMageFsmStates;

namespace Project.Scripts.Core
{
    public class SkeletonMage : SummonUnitBase
    {
        public override void Initialize()
        {
            Fsm.AddState(new SkeletonMageFsmStateIdle(Animator, Fsm));
            Fsm.AddState(new SkeletonMageFsmStateRun(Animator, Fsm));
            Fsm.AddState(new SkeletonMageFsmStateAttack(Animator, Fsm));
            
            Fsm.SetState<SkeletonMageFsmStateIdle>();
        }
    }
}