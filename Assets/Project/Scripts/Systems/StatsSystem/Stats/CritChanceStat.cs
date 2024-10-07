namespace Project.Scripts
{
    public class CritChanceStat : StatBase
    {
        public override string Name { get; set; } = "CritChance";
        public override StatType Type { get; set; } = StatType.CritChance;
        public override float Value { get; set; } = 0;
        public override float MaxValue { get; set; } = 100;
        public override float MinValue { get; set; } = 0;
        public override bool HasMaxValue { get; set; } = true;
        
        public override void InitializeValuesDefault()
        {
            InitializeValues(10, 100);
        }
    }
}