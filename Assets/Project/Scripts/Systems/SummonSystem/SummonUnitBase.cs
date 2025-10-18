using Project.Scripts.Core;
using Project.Scripts.Interfaces;
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
        protected AiMoveSystem AiMoveSystem = new();

        private void Start()
        {
            _playerController = PlayerControllerObject;
            AiMoveSystem.SetNavMeshAgent(GetComponent<NavMeshAgent>());
        }
    }
}