using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.FSM
{
    public class PlayerFsmStateSummon : FsmStateBase
    {
        private const string ANIMATION_NAME = "Summon";

        private readonly Animator _animator;
        private Fsm _fsm;
        private PlayerFsm _playerFsm;

        public PlayerFsmStateSummon(Fsm fsm, Animator animator, PlayerFsm playerFsm) :
            base(fsm)
        {
            _animator = animator;
            _fsm = fsm;
            _playerFsm = playerFsm;
        }
        
        public override async void Enter()
        {
            _playerFsm.IsPossibleToMove = false;
            _animator.Play(ANIMATION_NAME);
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