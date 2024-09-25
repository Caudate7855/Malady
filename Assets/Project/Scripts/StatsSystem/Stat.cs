namespace Project.Scripts
{
    public class Stat : IStat
    {
        public StatType Type { get; set; }
        public float Value { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }
        public bool HasMaxValue { get; set; }
    }
}