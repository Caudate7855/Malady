using Project.Scripts.Core;
using Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts
{
    public abstract class SummonUnitBase : AiBehaviourBase, ICustomInitializable
    {
        [Inject] public PlayerStats PlayerStats;
        
        public PlayerController PlayerControllerObject { get; set; }
        
        [SerializeField] protected Animator Animator;
        
        protected Fsm Fsm = new();
        protected AiMoveSystem AiMoveSystem = new();

        private PlayerController _playerController;

        private void Start()
        {
            _playerController = PlayerControllerObject;
            AiMoveSystem.SetNavMeshAgent(GetComponent<NavMeshAgent>());
        }

        public override void RotateToPoint(Vector3 point)
        {
            AiMoveSystem.RotateToPoint(point);
        }
    }
}