using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts
{
    public class EnemyRangeMoveState : EnemyFsmStateBase
    {
        
        
        public EnemyRangeMoveState(Fsm fsm, Animator animator, EnemyBase enemyController) : base(fsm, animator, enemyController)
        {
        }
        
        public override async void Enter()
        {
            EnemyController.CanChangeState = false;
            await UniTask.Delay(1500);
            EnemyController.CanChangeState = true;
        }

        public override void Exit()
        {
            
        }
        
        public override void Update()
        {
            
        }
    }
}