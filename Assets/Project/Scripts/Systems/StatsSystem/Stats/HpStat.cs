namespace Project.Scripts
{
    public class HpStat : StatBase
    {
        public override string Name { get; set; } = "Health";
        public override StatType Type { get; set; } = StatType.HP;
        public override float Value { get; set; } = 100;
        public override float MaxValue { get; set; } = 100;
        public override float MinValue { get; set; } = 0;
        public override bool HasMaxValue { get; set; } = true;
    }
}