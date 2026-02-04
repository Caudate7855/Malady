namespace Project.Scripts
{
    public class HpStat : StatBase
    {
        public override StatType StatType { get; protected set; }
        public override bool HasMinValue { get; protected set; }
        public override bool HasMaxValue { get; protected set; }
        public override float Value { get; protected set; }
        public override float MinValue { get; protected set; }
        public override float MaxValue { get; protected set; }
    }
}