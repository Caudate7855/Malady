namespace Project.Scripts
{
    public abstract class StatBase : IStat
    {
        public abstract string Name { get; set; }
        public abstract StatType Type { get; set; }
        public abstract float Value { get; set; }
        public abstract float MaxValue { get; set; }
        public abstract float MinValue { get; set; }
        public abstract bool HasMaxValue { get; set; }
    }
}