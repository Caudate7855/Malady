namespace Project.Scripts
{
    public class SummonSkeletonMageSpell : SpellBase
    {

        private SkeletonsCountStat _skeletonsCountStat;
        
        public override void Initialize()
        {
            ID  = "bones_0_0";
            IsInitialized = true;
            
            _skeletonsCountStat = PlayerStats.GetStat<SkeletonsCountStat>() as  SkeletonsCountStat;
        }
        
        public async override void Cast()
        {
            if (_skeletonsCountStat.Value < _skeletonsCountStat.MaxValue)
            {
                await SummonSystem.CreateSkeletonMageAsync(MouseController.MouseTarget.TargetPosition);
                _skeletonsCountStat.Value++;
            }
        }
    }
}