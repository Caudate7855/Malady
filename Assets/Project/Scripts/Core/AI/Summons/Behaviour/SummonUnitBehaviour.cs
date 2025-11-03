using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    public class SummonUnitBehaviour : GlobalAiBehaviour
    {
        public bool IsPlayerInFollowDistance;
        protected override float CycleDelay { get; set; } = 0.5f;

        [SerializeField] private RangeBehaviourChecker _followEnemyRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _attackRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _followPlayerRangeBehaviour;

        protected override void Initialize()
        {
            _followEnemyRangeBehaviour.Initialize<EnemyBase>();
            _attackRangeBehaviour.Initialize<EnemyBase>();
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

        private void OnFollowRangeBehaviourEnter(GameObject targetObject)
        {
            FollowObject = targetObject;
            IsOpponentInFollowDistance = true;
        }
        
        private void OnFollowRangeBehaviourExit()
        {
            IsOpponentInFollowDistance = false;
        }
        
        private void OnAttackRangeBehaviourEnter(GameObject targetObject)
        {
            AttackObject = targetObject;
            IsOpponentInAttackDistance = true;
            IsOpponentInFollowDistance = false;
        }
        
        private void OnAttackRangeBehaviourExit()
        {
            IsOpponentInAttackDistance = false;
            IsOpponentInFollowDistance = true;
        }

        private void OnFollowPlayerRangeBehaviourEnter(GameObject targetObject)
        {
            FollowObject = targetObject;
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
            AiBehaviourBase.RotateToPoint(AttackObject.transform.position);
            AiBehaviourBase.Attack();
        }

        public override void SetFollowBehaviour()
        {
            AiBehaviourBase.MoveTo(FollowObject.transform);
        }

        public override void SetIdleBehaviour()
        {
            AiBehaviourBase.Idle();
        }
    }
}