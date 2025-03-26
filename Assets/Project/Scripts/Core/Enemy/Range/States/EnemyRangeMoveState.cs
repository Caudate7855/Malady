using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class EnemyRangeMoveState : EnemyFsmStateBase
    {
        
        
        public EnemyRangeMoveState(Fsm fsm, Animator animator, EnemyBase enemyController) : base(fsm, animator, enemyController)
        {
        }
        
        public override async void Enter()
        {
            Debug.Log("move");

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