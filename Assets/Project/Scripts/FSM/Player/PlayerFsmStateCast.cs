using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerFsmStateCast : FsmStateBase
    {
        private const string AnimationName = "Cast";
        
        private readonly Animator _animator;
        private Fsm _fsm;
        private PlayerFsm _playerFsm;
        
        public PlayerFsmStateCast(Fsm fsm, Animator animator, PlayerFsm playerFsm) : base(fsm)
        {
            _animator = animator;
            _fsm = fsm;
            _playerFsm = playerFsm;
        }

        public override async void Enter()
        {
            _playerFsm.IsPossibleToMove = false;
            _animator.Play(AnimationName);
            await CastDelay();
        }

        private async UniTask CastDelay()
        {
            await UniTask.Delay(500);
            _playerFsm.IsPossibleToMove = true;
            _fsm.SetState<PlayerFsmStateIdle>();
        }
    }
}