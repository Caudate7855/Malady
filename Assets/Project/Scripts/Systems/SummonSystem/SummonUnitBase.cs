using Project.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts
{
    public abstract class SummonUnitBase : AiBehaviourBase
    {
        [SerializeField] protected Animator Animator;
        
        protected Fsm Fsm = new();
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