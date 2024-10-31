using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.FSM
{
    public class PlayerFsmStateCast : FsmStateBase
    {
        private const string AnimationName = "Cast";
        
        private readonly NavMeshAgent _playerNavMeshAgent;
        private readonly Animator _animator;
        private Fsm _fsm;
        
        public PlayerFsmStateCast(Fsm fsm, NavMeshAgent playerNavMeshAgent, Animator animator) : base(fsm)
        {
            _playerNavMeshAgent = playerNavMeshAgent;
            _animator = animator;
            _fsm = fsm;
        }

        public override async void Enter()
        {
            _animator.Play(AnimationName);
            await CastDelay();
            Exit();
        }

        private async UniTask CastDelay()
        {
            await UniTask.Delay(_animator.GetCurrentAnimatorClipInfo(0).Length * 1000);
        }
    }
}