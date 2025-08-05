using UnityEngine;

namespace Project.Scripts
{
    public class OverAttackableMouseState : FsmStateBase
    {
        public OverAttackableMouseState(Fsm fsm) : base(fsm)
        {
            
        }
        
        public override void Enter()
        {
            Debug.Log("Attackable Enter");
        }

        public override void Exit()
        {
            Debug.Log("Attackable Exit");
        }
    }
}