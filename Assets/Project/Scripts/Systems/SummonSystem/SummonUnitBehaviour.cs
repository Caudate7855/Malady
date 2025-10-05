using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    public class SummonUnitBehaviour : GlobalAiBehaviour
    {
        [SerializeField] private RangeBehaviourChecker _followRunRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _attackRangeBehaviour;
        
        private void OnEnable()
        {
            _followRunRangeBehaviour.OnTriggerEnterEvent += OnFollowRangeBehaviourEnter;
            _followRunRangeBehaviour.OnTriggerExitEvent += OnFollowRangeBehaviourExit;
            
            _attackRangeBehaviour.OnTriggerEnterEvent += OnAttackRangeBehaviourEnter;
            _attackRangeBehaviour.OnTriggerExitEvent += OnAttackRangeBehaviourExit;
        }
        
        private void OnDisable()
        {
            _followRunRangeBehaviour.OnTriggerEnterEvent -= OnFollowRangeBehaviourEnter;
            _followRunRangeBehaviour.OnTriggerExitEvent -= OnFollowRangeBehaviourExit;
            
            _attackRangeBehaviour.OnTriggerEnterEvent -= OnAttackRangeBehaviourEnter;
            _attackRangeBehaviour.OnTriggerExitEvent -= OnAttackRangeBehaviourExit;
        }
        
        protected override void Initialize()
        {
            _followRunRangeBehaviour.Initialize<PlayerController>();
            _attackRangeBehaviour.Initialize<PlayerController>();
        }

        private void OnFollowRangeBehaviourEnter()
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInFollowDistance = true;
            SetFollowRandomBehaviour();
        }
        
        private void OnFollowRangeBehaviourExit()
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInFollowDistance = false;
            SetIdleRandomBehaviour();
        }
        
        private void OnAttackRangeBehaviourEnter()
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInAttackDistance = true;
            IsOpponentInFollowDistance = false;
            SetAttackRandomBehaviour();
        }
        
        private void OnAttackRangeBehaviourExit()
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInAttackDistance = false;
            IsOpponentInFollowDistance = true;
            SetFollowRandomBehaviour();
        }

        protected override void TryChangeBehaviour()
        {
            if (IsOpponentInAttackDistance)
            {
                SetAttackRandomBehaviour();
                return;
            }

            if (IsOpponentInFollowDistance)
            {
                SetFollowRandomBehaviour();
                return;
            }

            SetIdleRandomBehaviour();
        }

        public override void  SetAttackRandomBehaviour()
        {
            _aiBehaviourBase.Attack();
        }

        public override void SetFollowRandomBehaviour()
        {
            _aiBehaviourBase.Move();
        }

        public override void SetIdleRandomBehaviour()
        {
            _aiBehaviourBase.Idle();
        }
    }
}