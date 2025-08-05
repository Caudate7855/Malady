using UnityEngine;

namespace Project.Scripts
{
    public class OverInteractableMouseState : FsmStateBase
    {
        public OverInteractableMouseState(Fsm fsm) : base(fsm)
        {
            
        }

        public override void Enter()
        {
            Debug.Log("Interactable Enter");
        }

        public override void Exit()
        {
            Debug.Log("Interactable Exit");
        }
    }
}