using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    public abstract class EnemyBase : AiBehaviourBase, IEnemy, ICustomInitializable
    {
        [SerializeField] protected Animator Animator;
        protected Fsm Fsm = new();
        
        public IPlayer Player { get; set; }

        public void Initialize()
        {
            InitializeFsm();
            Player = FindObjectOfType<PlayerController>();
        }
        
        protected abstract void InitializeFsm();
        public abstract void Idle();
        public abstract void Move();
        public abstract void Attack();

        private void Update()
        {
            Fsm.Update();
        }
    }
}