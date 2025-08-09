namespace Project.Scripts
{
    public class SkeletonWarriorsCountStat : StatBase
    {
        public override string Name { get; set; } = "SkeletonWarriorsCount";
        public override StatType Type { get; set; } = StatType.SkeletonWarriorsCountStat;
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