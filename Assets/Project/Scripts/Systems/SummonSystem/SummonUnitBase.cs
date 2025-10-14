using Project.Scripts.Core;
using Project.Scripts.Interfaces;
using Project.Scripts.States;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts
{
    public abstract class SummonUnitBase : AiBehaviourBase, ICustomInitializable
    {
        [Inject] public IStatSystem StatsSystem;
        
        public PlayerController PlayerControllerObject { get; set; }
        [SerializeField] private PlayerController _playerController;

        [SerializeField] protected Animator Animator;
        [SerializeField] private NavMeshAgent _agent;

        protected Fsm Fsm = new();

        private void Start()
        {
            _playerController = PlayerControllerObject;
        }

        public virtual void Initialize()
        {
            Fsm.AddState(new SummonUnitFsmStateRun(Animator, Fsm));
            Fsm.AddState(new SummonUnitFsmStateIdle(Animator, Fsm));
            Fsm.AddState(new SummonUnitFsmStateAttack(Animator, Fsm));
        }

        public override void Attack()
        {
            Fsm.SetState<SummonUnitFsmStateAttack>();
            Debug.Log("Summon unit attack");
        }

        public override void Idle()
        {
            Fsm.SetState<SummonUnitFsmStateIdle>();
            Debug.Log("Summon unit idle");
        }

        public override void Move()
        {
            Fsm.SetState<SummonUnitFsmStateRun>();
            Debug.Log("Summon unit move");
        }

        public override void MoveToPlayer()
        {
            Fsm.SetState<SummonUnitFsmStateRun>();
            Debug.Log("Summon unit follow player");
        }
        
        public virtual void MoveToPoint()
        {
            Fsm.SetState<SummonUnitFsmStateRun>();
            Debug.Log("Summon unit move to point");
        }
    }
}