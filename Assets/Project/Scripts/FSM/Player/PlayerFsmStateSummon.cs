using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerFsmStateSummon : FsmStateBase
    {
        private const string ANIMATION_NAME = "Summon";

        private readonly Animator _animator;
        private Fsm _fsm;
        private PlayerFsm _playerFsm;
        private int _castDurationInMilliseconds = 1000;

        public PlayerFsmStateSummon(Fsm fsm, Animator animator, PlayerFsm playerFsm) :
            base(fsm)
        {
            _animator = animator;
            _fsm = fsm;
            _playerFsm = playerFsm;
        }

        public override void Enter()
        {
            _playerFsm.IsPossibleToMove = false;
            _animator.Play(ANIMATION_NAME);

            CastAndExit().Forget();
        }

        private async UniTaskVoid CastAndExit()
        {
            await UniTask.Delay(_castDurationInMilliseconds);

            _playerFsm.IsPossibleToMove = true;
            _fsm.SetState<PlayerFsmStateIdle>();
        }
    }
}