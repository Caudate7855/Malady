namespace Project.Scripts
{
    public class CritDamageStat : StatBase
    {
        public override string Name { get; set; } = "CritDamage";
        public override StatType Type { get; set; } = StatType.CritDamage;
        public override float Value { get; set; } = 100;
        public override float MaxValue { get; set; } = 500;
        public override float MinValue { get; set; } = 100;
        public override bool HasMaxValue { get; set; } = true;
        
        public override void InitializeValuesDefault()
        {
            InitializeValues(0, 100);
        }
    }
}