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
        public abstract void InitializeValuesDefault();
        
        public void Update(float newValue)
        {
            Value =  newValue;
        }


        public virtual void InitializeValues(float value, float maxValue = default)
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
    }
}