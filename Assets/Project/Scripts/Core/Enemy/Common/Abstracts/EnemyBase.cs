using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    public abstract class EnemyBase : AiBehaviourBase, IEnemy, ICustomInitializable
    {
        [SerializeField] protected Animator Animator;
        protected Fsm Fsm = new();
        
        public PlayerController Player { get; set; }
        

        public void Initialize()
        {
            InitializeFsm();
            Player = FindObjectOfType<PlayerController>();
        }
        
        protected abstract void InitializeFsm();

        private void Update()
        {
            Fsm.Update();
        }
    }
}