namespace Project.Scripts
{
    public class SkeletonsCountStat : StatBase
    
    {
        public override string Name { get; set; }
        public override StatType Type { get; set; }
        public override float Value { get; set; }
        public override float MaxValue { get; set; }
        public override float MinValue { get; set; }
        public override bool HasMaxValue { get; set; }
        public override void InitializeValuesDefault()
        {
            
        }
    }
}