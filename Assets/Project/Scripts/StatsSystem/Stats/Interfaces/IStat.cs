namespace Project.Scripts
{
    public interface IStat
    {
        public StatType Type { get; set; }
        public float Value { get; set; }
        public float MaxValue { get; set; }
        public float MinValue { get; set; }

        public bool HasMaxValue { get; set; }

        public void Initialize(float value, float maxValue = default)
        {
            MinValue = 0;
            Value = value;

            if (maxValue != default)
            {
                HasMaxValue = true;
                MaxValue = maxValue;
            }
            else
            {
                HasMaxValue = false;
                MaxValue = float.MaxValue;
            }
        }

        public void ChangeMaxValue(float newValue)
        {
            if (HasMaxValue)
            {
                MaxValue = newValue;
            }
        }
        
        public void Increment(float amount)
        {
            if (amount + Value >= MaxValue && HasMaxValue)
            {
                Value = MaxValue;
            }
            else
            {
                Value += amount;
            }
        }

        public void Decrement(float amount)
        {
            if (Value - amount <= MinValue)
            {
                Value = MinValue;
            }
            else
            {
                Value -= amount;
            }
        }
    }
}