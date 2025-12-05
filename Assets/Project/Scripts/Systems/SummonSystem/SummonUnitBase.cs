using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts
{
    public abstract class SummonUnitBase : AiBehaviourBase, IInitializable
    {
        [SerializeField] protected Animator Animator;
        
        protected Fsm Fsm = new();

        public abstract void Initialize();

        protected AiMoveSystem AiMoveSystem = new();

        private void Start()
        {
            AiMoveSystem.SetNavMeshAgent(GetComponent<NavMeshAgent>());
        }

        public override void RotateToPoint(Vector3 point)
        {
            AiMoveSystem.RotateToPoint(point);
        }
    }
}