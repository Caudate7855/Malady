namespace Project.Scripts
{
    public class MagicResistStat : StatBase
    {
        public override string Name { get; set; } = "MagicResist";
        public override StatType Type { get; set; } = StatType.MagicResist;
        public override float Value { get; set; }
        public override float MaxValue { get; set; }
        public override float MinValue { get; set; }
        public override bool HasMaxValue { get; set; }
        
        public override void InitializeValuesDefault()
        {
            InitializeValues(0, 100);
        }
    }
}