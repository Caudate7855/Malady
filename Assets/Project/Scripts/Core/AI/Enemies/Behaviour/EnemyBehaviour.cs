using R3;
using UnityEngine;

namespace Project.Scripts
{
    public class EnemyBehaviour : GlobalAiBehaviour
    {
        [SerializeField] private RangeBehaviourChecker _followRunRangeBehaviour;
        [SerializeField] private RangeBehaviourChecker _attackRangeBehaviour;
        
        private AiBehaviourBase _aiBehaviour;

        protected override void Initialize()
        {
            _followRunRangeBehaviour.Initialize<PlayerController>();
            _attackRangeBehaviour.Initialize<PlayerController>();
            
            _followRunRangeBehaviour.IsInRange.
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

            IsOpponentInFollowDistance = values.condition;
            FollowObject = values.target;
        }

        private void OnAttackRangeBehaviourChanged((bool condition, GameObject target) values)
        {
            if (!IsAiEnabled)
            {
                return;
            }
            
            IsOpponentInAttackDistance = values.condition;
            FollowObject = values.target;
        }
    }
}