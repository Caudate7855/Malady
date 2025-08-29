using Project.Scripts.Core;
using Project.Scripts.Interfaces;
using Project.Scripts.States;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts
{
    public abstract class SummonUnitBase : MonoBehaviour, ICustomInitializable
    {
        [Inject] public IStatSystem StatsSystem;
        
        public PlayerController PlayerControllerObject { get; set; }
        [SerializeField] private PlayerController _playerController;

        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;

        protected Fsm Fsm = new();

        private void Start()
        {
            _playerController = PlayerControllerObject;
        }

        public void Initialize()
        {
            Fsm.AddState(new SummonUnitFsmStateRun(_animator, Fsm));
            Fsm.AddState(new SummonUnitFsmStateIdle(_animator, Fsm));
            Fsm.AddState(new SummonUnitFsmStateAttack(_animator, Fsm));
        }

        public void Attack()
        {
            Fsm.SetState<SummonUnitFsmStateAttack>();
        }

        public void Idle()
        {
            Fsm.SetState<SummonUnitFsmStateIdle>();
        }

        public void MoveToPoint()
        {
            Fsm.SetState<SummonUnitFsmStateRun>();
        }
    }
}