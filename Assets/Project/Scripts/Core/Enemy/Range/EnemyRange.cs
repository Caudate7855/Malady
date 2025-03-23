namespace Project.Scripts.Core
{
    public class EnemyRange : EnemyBase
    {
        protected override void InitializeFsm()
        {
            Fsm.AddState(new EnemyRangeIdleState(Fsm, Animator));
            Fsm.AddState(new EnemyRangeMoveState(Fsm, Animator));
            
            Fsm.SetState<EnemyRangeIdleState>();
        }

        public override void Idle()
        {
            
        }

        public override void Move()
        {
            
        }

        public override void Attack()
        {
            
        }
    }
}