namespace Project.Scripts
{
    public class BloodSpearBonusDamageStat : StatBase
    {
        public override string Name { get; set; } = "Blood lance bonus damage";
        public override StatType Type { get; set; } = StatType.BloodSpearDamageStat;
        public override float Value { get; set; }
        public override float MaxValue { get; set; }
        public override float MinValue { get; set; }
        public override bool HasMaxValue { get; set; } = false;
        public override void InitializeValuesDefault()
        {
            InitializeValues(100);
        }
    }
}