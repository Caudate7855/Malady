using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyRangeAttackState : EnemyFsmStateBase
    {
        public EnemyRangeAttackState(Fsm fsm, Animator animator, EnemyBase enemyController) : base(fsm, animator, enemyController)
        {
            
        }
        
        public override void Enter()
        {
            Debug.Log($"EnemyRange - state - ATTACK");
        }

        public override void Exit()
        {
            
        }
        
        public override void Update()
        {
            
        }
    }
}