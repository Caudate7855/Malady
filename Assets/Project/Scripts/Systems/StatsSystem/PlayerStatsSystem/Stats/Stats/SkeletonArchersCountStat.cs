namespace Project.Scripts
{
    public class SkeletonArchersCountStat : StatBase
    {
        public override string Name { get; set; } =  "SkeletonArchersCount";
        public override StatType Type { get; set; } = StatType.SkeletonArchersCountStat;
        public override float Value { get; set; } = 0f;
        public override float MaxValue { get; set; } = 3f;
        public override float MinValue { get; set; } = 0f;
        public override bool HasMaxValue { get; set; } = true;
        public override void InitializeValuesDefault()
        {
            InitializeValues(0, 3);
        }
    }
}