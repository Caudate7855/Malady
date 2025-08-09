using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts
{
    public class PlayerFsmStateIdle : FsmStateBase
    {
        private const string AnimationName = "Idle";
        
        private readonly NavMeshAgent _playerNavMeshAgent;
        private readonly Animator _animator;
        
        public PlayerFsmStateIdle(Fsm fsm, NavMeshAgent playerNavMeshAgent, Animator animator) : base(fsm)
        {
            _playerNavMeshAgent = playerNavMeshAgent;
            _animator = animator;
        }

        public override void Enter()
        {
           // _animator.Play(AnimationName);
        }
    }
}