using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyFsmStateBase : FsmStateBase
    {
        protected Animator Animator;
        protected EnemyBase EnemyController;
        
        public EnemyFsmStateBase(Fsm fsm, Animator animator, EnemyBase enemyController) : base(fsm)
        {
            Animator = animator;
            EnemyController = enemyController;
        }
    }
}