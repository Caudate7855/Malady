namespace Project.Scripts
{
    public class EssenceStat : StatBase
    {
        public override string Name { get; set; } = "Essence";
        public override StatType Type { get; set; } = StatType.Essence;
        public override float Value { get; set; } = 100;
        public override float MaxValue { get; set; } = 100;
        public override float MinValue { get; set; } = 0;
        public override bool HasMaxValue { get; set; } = true;
        
        public override void InitializeValuesDefault()
        {
            InitializeValues(100, 100);
        }
    }
}