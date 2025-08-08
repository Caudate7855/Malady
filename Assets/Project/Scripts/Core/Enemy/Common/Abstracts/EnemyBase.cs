using Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core
{
    public abstract class EnemyBase : AiBehaviourBase, IEnemy, ICustomInitializable
    {
        public bool CanChangeState = true;

        [SerializeField] protected Animator Animator;
        protected Fsm Fsm = new();
        protected EnemyMoveSystem _enemyMoveSystem = new();

        public PlayerController PlayerControllerObject { get; set; }

        public void Initialize()
        {
            InitializeFsm();
            _enemyMoveSystem.SetNavMeshAgent(GetComponent<NavMeshAgent>());
        }

        protected abstract void InitializeFsm();
        
        private void Update()
        {
            Fsm.Update();
        }
    }
}