using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyRangePatrolState : EnemyFsmStateBase
    {
        public EnemyRangePatrolState(Fsm fsm, Animator animator, EnemyBase enemyController) : base(fsm, animator, enemyController)
        {
            
        }
        
        public override void Enter()
        {
            //Debug.Log("patrol");
        }

        public override void Exit()
        {
            
        }
        
        public override void Update()
        {
            
        }
    }
}