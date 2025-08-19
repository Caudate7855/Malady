using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyBehaviour : GlobalAiBehaviour
    {
        [SerializeField] private RangeBehaviourChecker followRunRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker attackRangeBehaviour;
        
        private  AiBehaviourBase _aiBehaviour;

        private void OnEnable()
        {
            followRunRangeBehaviour.OnTriggerEnterEvent += OnFollowRangeBehaviourEnter;
            followRunRangeBehaviour.OnTriggerExitEvent += OnFollowRangeBehaviourExit;
            
            attackRangeBehaviour.OnTriggerEnterEvent += OnAttackRangeBehaviourEnter;
            attackRangeBehaviour.OnTriggerExitEvent += OnAttackRangeBehaviourExit;
        }
        
        private void OnDisable()
        {
            followRunRangeBehaviour.OnTriggerEnterEvent -= OnFollowRangeBehaviourEnter;
            followRunRangeBehaviour.OnTriggerExitEvent -= OnFollowRangeBehaviourExit;
            
            attackRangeBehaviour.OnTriggerEnterEvent -= OnAttackRangeBehaviourEnter;
            attackRangeBehaviour.OnTriggerExitEvent -= OnAttackRangeBehaviourExit;
        }
        
        protected override void Initialize()
        {
            var player = FindFirstObjectByType<PlayerController>();
            followRunRangeBehaviour.Initialize(player);
            attackRangeBehaviour.Initialize(player);
        }

        private void OnFollowRangeBehaviourEnter()
        {
            IsOpponentInFollowDistance = true;
            SetFollowRandomBehaviour();
        }
        
        private void OnFollowRangeBehaviourExit()
        {
            IsOpponentInFollowDistance = false;
            SetIdleRandomBehaviour();
        }
        
        private void OnAttackRangeBehaviourEnter()
        {
            IsOpponentInAttackDistance = true;
            IsOpponentInFollowDistance = false;
            SetAttackRandomBehaviour();
        }
        
        private void OnAttackRangeBehaviourExit()
        {
            IsOpponentInAttackDistance = false;
            IsOpponentInFollowDistance = true;
            SetFollowRandomBehaviour();
        }
    }
}