namespace Project.Scripts
{
    public class SkeletonsCountStat : StatBase
    
    {
        public override string Name { get; set; } = "SkeletonMagesCount";
        public override StatType Type { get; set; } = StatType.SkeletonsCountStat;
        public override float Value { get; set; }
        public override float MaxValue { get; set; }
        public override float MinValue { get; set; } = 0;
        public override bool HasMaxValue { get; set; } = true;
        public override void InitializeValuesDefault()
        {
            InitializeValues(0, 3);
        }
    }
}