using Project.Scripts.Services;
using R3;
using UnityEngine;

namespace Project.Scripts.Summons
{
    public class SummonUnitBehaviour : GlobalAiBehaviour
    {
        public bool IsPlayerInFollowDistance;
        protected override float CycleDelay { get; set; } = 0.1f;

        [SerializeField] private RangeBehaviourChecker _attackRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _followPlayerRangeBehaviour;

        protected override void Initialize()
        {
            _followPlayerRangeBehaviour.Initialize<PlayerController>();
            _attackRangeBehaviour.Initialize<PlayerController>();
            
            _followPlayerRangeBehaviour.IsInRange.
                Subscribe(OnFollowRangeBehaviourChanged).
                AddTo(CompositeDisposable);
            
            _attackRangeBehaviour.IsInRange.
                Subscribe(OnAttackRangeBehaviourChanged).
                AddTo(CompositeDisposable);
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

        private void OnAttackRangeBehaviourEnter(GameObject targetObject)
        {
            AttackObject = targetObject;
            IsOpponentInAttackDistance = true;
        }
        
        private void OnAttackRangeBehaviourExit()
        {
            IsOpponentInAttackDistance = false;
        }

        private void OnFollowPlayerRangeBehaviourEnter(GameObject targetObject)
        {
            IsPlayerInFollowDistance = true;
        }

        private void OnFollowPlayerRangeBehaviourExit()
        {
            IsPlayerInFollowDistance = false;
        }

        protected override void TryChangeBehaviour()
        {
            if (!IsPlayerInFollowDistance)
            {
                SetFollowPlayerBehaviour();
                return;
            }
            
            if (IsOpponentInAttackDistance)
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
            AiBehaviourBase.RotateToPoint(AttackObject.transform.position);
            AiBehaviourBase.Attack();
        }

        public override void SetIdleBehaviour()
        {
            AiBehaviourBase.Idle();
        }
    }
}