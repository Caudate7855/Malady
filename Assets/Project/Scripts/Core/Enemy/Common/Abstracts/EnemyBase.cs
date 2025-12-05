using Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts
{
    public abstract class EnemyBase : AiBehaviourBase, IEnemy
    {
        public bool CanChangeState = true;

        [SerializeField] protected Animator Animator;
        protected Fsm Fsm = new();
        protected AiMoveSystem AiMoveSystem = new();
        protected IStatSystem StatSystem = new PlayerStats();

        public void Start()
        {
            AiMoveSystem.SetNavMeshAgent(GetComponent<NavMeshAgent>());
            InitializeFsm();
        }
        
        protected abstract void InitializeFsm();
        
        private void Update()
        {
            Fsm.Update();
        }

        public virtual void TakeDamage(float damageToTake)
        {
            var currentHp = StatSystem.GetStat<HpStat>().Value;
            var newHpValue = currentHp - damageToTake;
            
            StatSystem.UpdateStat<HpStat>(newHpValue);
            Debug.Log(StatSystem.GetStat<HpStat>().Value);
        }
    }
}