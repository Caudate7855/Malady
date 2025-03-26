using Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core
{
    public abstract class EnemyBase : AiBehaviourBase, IEnemy, ICustomInitializable
    {
        public bool CanChangeState = true;
        public PlayerController Player { get; set; }

        [SerializeField] protected Animator Animator;
        protected Fsm Fsm = new();
        protected EnemyMoveSystem _enemyMoveSystem = new();

        public void Initialize()
        {
            InitializeFsm();
            _enemyMoveSystem.SetNavMeshAgent(GetComponent<NavMeshAgent>());
            Player = FindObjectOfType<PlayerController>();
        }
        
        protected abstract void InitializeFsm();
        
        private void Update()
        {
            Fsm.Update();
        }
    }
}