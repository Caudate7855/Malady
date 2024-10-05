namespace Project.Scripts
{
    public class DamageStat : StatBase
    {
        public override string Name { get; set; } = "Damage";
        public override StatType Type { get; set; } = StatType.Damage;
        public override float Value { get; set; } = 10;
        public override float MaxValue { get; set; } = 0;
        public override float MinValue { get; set; } = 2;
        public override bool HasMaxValue { get; set; } = false;
    }
}