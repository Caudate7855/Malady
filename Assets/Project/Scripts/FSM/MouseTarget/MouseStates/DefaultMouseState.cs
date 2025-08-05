using UnityEngine;

namespace Project.Scripts
{
    public class DefaultMouseState : FsmStateBase
    {
        public DefaultMouseState(Fsm fsm) : base(fsm)
        {
            
        }
        
        public override void Enter()
        {
            Debug.Log("Default Enter");
        }

        public override void Exit()
        {
            Debug.Log("Default Exit");
        }
    }
}