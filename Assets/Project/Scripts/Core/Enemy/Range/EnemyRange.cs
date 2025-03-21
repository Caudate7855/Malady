namespace Project.Scripts.Core
{
    public class EnemyRange : EnemyBase
    {
        protected override void InitializeFsm()
        {
            _fsm.AddState(new EnemyRangeIdleState(_fsm));
            _fsm.AddState(new EnemyRangeMoveState(_fsm));
            
            _fsm.SetState<EnemyRangeIdleState>();
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