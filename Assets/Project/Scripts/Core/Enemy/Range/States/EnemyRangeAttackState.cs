using UnityEngine;

namespace Project.Scripts
{
    public class EnemyRangeAttackState : EnemyFsmStateBase
    {
        public EnemyRangeAttackState(Fsm fsm, Animator animator, EnemyBase enemyController) : base(fsm, animator, enemyController)
        {
            
        }
        
        public override void Enter()
        {
            Debug.Log("attack");
        }

        public override void Exit()
        {
            
        }
        
        public override void Update()
        {
            
        }
    }
}