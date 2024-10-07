namespace Project.Scripts
{
    public class AttackSpeedStat : StatBase
    {
        public override string Name { get; set; } = "AttackSpeed";
        public override StatType Type { get; set; } = StatType.AttackSpeed;
        public override float Value { get; set; } = 100;
        public override float MaxValue { get; set; } = 200;
        public override float MinValue { get; set; } = 100;
        public override bool HasMaxValue { get; set; } = true;
        
        public override void InitializeValuesDefault()
        {
            InitializeValues(100, 300);
        }
    }
}