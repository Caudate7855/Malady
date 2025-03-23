using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    public abstract class EnemyBase : MonoBehaviour, IEnemy, ICustomInitializable
    {
        [SerializeField] protected Animator Animator;
        protected Fsm Fsm;
        
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
    }
}