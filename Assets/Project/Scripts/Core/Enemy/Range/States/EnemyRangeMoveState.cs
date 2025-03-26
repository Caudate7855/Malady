using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyRangeMoveState : EnemyFsmStateBase
    {
        public EnemyRangeMoveState(Fsm fsm, Animator animator, EnemyBase enemyController) : base(fsm, animator, enemyController)
        {
        }
        
        public override void Enter()
        {
            Debug.Log("move");
        }

        public override void Exit()
        {
            
        }
        
        public override void Update()
        {
            
        }
    }
}