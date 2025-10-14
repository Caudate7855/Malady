using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    public class SummonUnitBehaviour : GlobalAiBehaviour
    {
        [SerializeField] private RangeBehaviourChecker _followEnemyRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _attackRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _followPlayerRangeBehaviour;

        public bool IsPlayerInFollowDistance;

        protected override void Initialize()
        {
            _followEnemyRangeBehaviour.Initialize<PlayerController>();
            _attackRangeBehaviour.Initialize<PlayerController>();
            _followPlayerRangeBehaviour.Initialize<PlayerController>();
        }

        private void OnEnable()
        {
            _followEnemyRangeBehaviour.OnTriggerEnterEvent += OnFollowRangeBehaviourEnter;
            _followEnemyRangeBehaviour.OnTriggerExitEvent += OnFollowRangeBehaviourExit;
            
            _attackRangeBehaviour.OnTriggerEnterEvent += OnAttackRangeBehaviourEnter;
            _attackRangeBehaviour.OnTriggerExitEvent += OnAttackRangeBehaviourExit;

            _followPlayerRangeBehaviour.OnTriggerEnterEvent += OnFollowPlayerRangeBehaviourEnter;
            _followPlayerRangeBehaviour.OnTriggerExitEvent += OnFollowPlayerRangeBehaviourExit;
        }

        private void OnDisable()
        {
            _followEnemyRangeBehaviour.OnTriggerEnterEvent -= OnFollowRangeBehaviourEnter;
            _followEnemyRangeBehaviour.OnTriggerExitEvent -= OnFollowRangeBehaviourExit;
            
            _attackRangeBehaviour.OnTriggerEnterEvent -= OnAttackRangeBehaviourEnter;
            _attackRangeBehaviour.OnTriggerExitEvent -= OnAttackRangeBehaviourExit;
            
            _followPlayerRangeBehaviour.OnTriggerEnterEvent -= OnFollowPlayerRangeBehaviourEnter;
            _followPlayerRangeBehaviour.OnTriggerExitEvent -= OnFollowPlayerRangeBehaviourExit;
        }

        private void OnFollowRangeBehaviourEnter()
        {
            IsOpponentInFollowDistance = true;
            SetFollowBehaviour();
        }
        
        private void OnFollowRangeBehaviourExit()
        {
            IsOpponentInFollowDistance = false;
            SetIdleBehaviour();
        }
        
        private void OnAttackRangeBehaviourEnter()
        {
            IsOpponentInAttackDistance = true;
            IsOpponentInFollowDistance = false;
            SetAttackBehaviour();
        }
        
        private void OnAttackRangeBehaviourExit()
        {
            IsOpponentInAttackDistance = false;
            IsOpponentInFollowDistance = true;
            SetFollowBehaviour();
        }

        private void OnFollowPlayerRangeBehaviourEnter()
        {
            IsPlayerInFollowDistance = true;
            SetIdleBehaviour();
        }

        private void OnFollowPlayerRangeBehaviourExit()
        {
            IsPlayerInFollowDistance = false;
            SetFollowPlayerBehaviour();
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

            if (IsOpponentInFollowDistance)
            {
                SetFollowBehaviour();
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
            AiBehaviourBase.Attack();
        }

        public override void SetFollowBehaviour()
        {
            AiBehaviourBase.Move();
        }

        public override void SetIdleBehaviour()
        {
            AiBehaviourBase.Idle();
        }
    }
}