using Project.Scripts.Player;
using R3;
using UnityEngine;

namespace Project.Scripts.Summons
{
    public class SummonUnitBehaviour : GlobalAiBehaviour
    {
        public bool IsPlayerInFollowDistance;
        protected override float CycleDelay => _cycleDelayInSeconds;
        [SerializeField] protected float _cycleDelayInSeconds = 0.5f;

        [SerializeField] private RangeBehaviourChecker _attackRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _followPlayerRangeBehaviour;

        protected override void Initialize()
        {
            _followPlayerRangeBehaviour.Initialize<PlayerController>();
            _attackRangeBehaviour.Initialize<EnemyBase>();

            _followPlayerRangeBehaviour
                .IsInRange
                .Subscribe(OnFollowRangeBehaviourChanged)
                .AddTo(CompositeDisposable);

            _attackRangeBehaviour
                .IsInRange
                .Subscribe(OnAttackRangeBehaviourChanged)
                .AddTo(CompositeDisposable);
        }

        private void OnFollowRangeBehaviourChanged((bool condition, GameObject target) values)
        {
            if (!IsAiEnabled)
            {
                return;
            }

            IsPlayerInFollowDistance = values.condition;
            FollowObject = values.target;
        }

        private void OnAttackRangeBehaviourChanged((bool condition, GameObject target) values)
        {
            if (!IsAiEnabled)
            {
                return;
            }

            IsOpponentInAttackDistance = values.condition;
            AttackObject = values.target;
        }

        protected override void TryChangeBehaviour()
        {
            if (!IsAiEnabled)
            {
                return;
            }

            if (!IsPlayerInFollowDistance)
            {
                SetFollowPlayerBehaviour();
                return;
            }
            
            if (IsOpponentInAttackDistance && AttackObject != null)
            {
                SetAttackBehaviour();
                return;
            }

            SetIdleBehaviour();
        }

        public void SetFollowPlayerBehaviour()
        {
            AiBehaviourBase.MoveToPlayer();
        }

        public override void SetAttackBehaviour()
        {
            if (AttackObject == null)
            {
                SetIdleBehaviour();
                return;
            }

            AiBehaviourBase.RotateToPoint(AttackObject.transform.position);
            AiBehaviourBase.Attack();
        }

        public override void SetIdleBehaviour()
        {
            AiBehaviourBase.Idle();
        }
    }
}
