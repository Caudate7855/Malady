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

        private int _castDurationInMilliseconds = 1000;
        
        public PlayerFsmStateCast(Fsm fsm, Animator animator, PlayerFsm playerFsm) : base(fsm)
        {
            _animator = animator;
            _fsm = fsm;
            _playerFsm = playerFsm;
        }

        public override async void Enter()
        {
            _playerFsm.IsPossibleToMove = false;
            _animator.CrossFade(AnimationName, 0.25f);
            await CastDelay();
        }

        private async UniTask CastDelay()
        {
            await UniTask.Delay(_castDurationInMilliseconds);
            _playerFsm.IsPossibleToMove = true;
            _fsm.SetState<PlayerFsmStateIdle>();
        }
    }
}