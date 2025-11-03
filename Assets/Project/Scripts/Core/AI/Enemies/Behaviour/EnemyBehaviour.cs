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
            followRunRangeBehaviour.Initialize<PlayerController>();
            attackRangeBehaviour.Initialize<PlayerController>();
        }

        private void OnFollowRangeBehaviourEnter(GameObject targetObject)
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInFollowDistance = true;
            SetFollowBehaviour();
        }
        
        private void OnFollowRangeBehaviourExit()
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInFollowDistance = false;
            SetIdleBehaviour();
        }
        
        private void OnAttackRangeBehaviourEnter(GameObject targetObject)
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInAttackDistance = true;
            IsOpponentInFollowDistance = false;
            SetAttackBehaviour();
        }
        
        private void OnAttackRangeBehaviourExit()
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInAttackDistance = false;
            IsOpponentInFollowDistance = true;
            SetFollowBehaviour();
        }
    }
}