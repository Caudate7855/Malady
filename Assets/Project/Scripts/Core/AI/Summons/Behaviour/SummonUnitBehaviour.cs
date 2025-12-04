using UnityEngine;

namespace Project.Scripts.Core.Summons
{
    public class SummonUnitBehaviour : GlobalAiBehaviour
    {
        public bool IsPlayerInFollowDistance;
        protected override float CycleDelay { get; set; } = 0.1f;

        [SerializeField] private RangeBehaviourChecker _attackRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _followPlayerRangeBehaviour;

        protected override void Initialize()
        {
            _attackRangeBehaviour.Initialize<EnemyBase>();
            _followPlayerRangeBehaviour.Initialize<PlayerController>();
        }

        private void OnEnable()
        {
            _attackRangeBehaviour.OnTriggerEnterEvent += OnAttackRangeBehaviourEnter;
            _attackRangeBehaviour.OnTriggerExitEvent += OnAttackRangeBehaviourExit;

            _followPlayerRangeBehaviour.OnTriggerEnterEvent += OnFollowPlayerRangeBehaviourEnter;
            _followPlayerRangeBehaviour.OnTriggerExitEvent += OnFollowPlayerRangeBehaviourExit;
        }

        private void OnDisable()
        {
            _attackRangeBehaviour.OnTriggerEnterEvent -= OnAttackRangeBehaviourEnter;
            _attackRangeBehaviour.OnTriggerExitEvent -= OnAttackRangeBehaviourExit;
            
            _followPlayerRangeBehaviour.OnTriggerEnterEvent -= OnFollowPlayerRangeBehaviourEnter;
            _followPlayerRangeBehaviour.OnTriggerExitEvent -= OnFollowPlayerRangeBehaviourExit;
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