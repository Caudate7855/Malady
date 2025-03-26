using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyBehaviour : GlobalAiBehaviour
    {
        [SerializeField] private TriggerChecker _followRunTrigger;
        [SerializeField] private TriggerChecker _attackTrigger;
        
        private  AiBehaviourBase _aiBehaviour;

        private void OnEnable()
        {
            _followRunTrigger.OnTriggerEnterEvent += OnFollowTriggerEnter;
            _followRunTrigger.OnTriggerExitEvent += OnFollowTriggerExit;
            
            _attackTrigger.OnTriggerEnterEvent += OnAttackTriggerEnter;
            _attackTrigger.OnTriggerExitEvent += OnAttackTriggerExit;
        }
        
        private void OnDisable()
        {
            _followRunTrigger.OnTriggerEnterEvent -= OnFollowTriggerEnter;
            _followRunTrigger.OnTriggerExitEvent -= OnFollowTriggerExit;
            
            _attackTrigger.OnTriggerEnterEvent -= OnAttackTriggerEnter;
            _attackTrigger.OnTriggerExitEvent -= OnAttackTriggerExit;
        }
        
        protected override void Initialize()
        {
            var player = FindObjectOfType<PlayerController>();
            _followRunTrigger.Initialize(player);
            _attackTrigger.Initialize(player);
        }

        private void OnFollowTriggerEnter()
        {
            IsOpponentInFollowDistance = true;
            SetFollowRandomBehaviour();
        }
        
        private void OnFollowTriggerExit()
        {
            IsOpponentInFollowDistance = false;
            SetIdleRandomBehaviour();
        }
        
        private void OnAttackTriggerEnter()
        {
            IsOpponentInAttackDistance = true;
            IsOpponentInFollowDistance = false;
            SetAttackRandomBehaviour();
        }
        
        private void OnAttackTriggerExit()
        {
            IsOpponentInAttackDistance = false;
            IsOpponentInFollowDistance = true;
            SetFollowRandomBehaviour();
        }
    }
}