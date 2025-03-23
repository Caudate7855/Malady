using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyRange : EnemyBase
    {
        [SerializeField] private TriggerChecker _followRunTrigger;
        [SerializeField] private TriggerChecker _attackTrigger;

        private void Awake()
        {
            _followRunTrigger.Initialize(FindObjectOfType<PlayerController>());
            _attackTrigger.Initialize(FindObjectOfType<PlayerController>());
        }

        private void OnEnable()
        {
            _followRunTrigger.OnTriggerEnterEvent += Move;
            _followRunTrigger.OnTriggerExitEvent += Idle;
            
            _attackTrigger.OnTriggerEnterEvent += Attack;
            _attackTrigger.OnTriggerEnterEvent += Move;
        }
        
        private void OnDisable()
        {
            _followRunTrigger.OnTriggerEnterEvent -= Move;
            _followRunTrigger.OnTriggerExitEvent -= Idle;
            
            _attackTrigger.OnTriggerEnterEvent -= Attack;
            _attackTrigger.OnTriggerEnterEvent -= Move;
        }

        protected override void InitializeFsm()
        {
            Fsm.AddState(new EnemyRangeIdleState(Fsm, Animator, this));
            Fsm.AddState(new EnemyRangeMoveState(Fsm, Animator, this));
            Fsm.AddState(new EnemyRangeAttackState(Fsm, Animator, this));
            
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void Idle()
        {
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void Move()
        {
            Fsm.SetState<EnemyRangeMoveState>();
        }

        public override void Attack()
        {
            Fsm.SetState<EnemyRangeAttackState>();
        }
    }
}