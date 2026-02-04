using System;
using R3;

namespace Project.Scripts
{
    public abstract class StatBase : IStat
    {
        public enum StatValueType
        {
            Value,
            MinValue,
            MaxValue
        }
        
        public abstract StatType StatType { get; protected set; }
        
        public abstract bool HasMinValue { get; protected set; }
        public abstract bool HasMaxValue { get; protected set; }

        public abstract float Value { get; protected set; }
        public abstract float MinValue { get; protected set; }
        public abstract float MaxValue { get; protected set; }

        public ReactiveCommand<IStat> OnStatChanged { get; } = new();

        public virtual void Init(StatConfig config)
        {
            InitInternal(config);
        }

        public virtual void ChangeValue(StatValueType statValueType, float newValue)
        {
            switch (statValueType)
            {
                case StatValueType.Value:
                    Value = newValue;
                    break;
                
                case StatValueType.MinValue:
                    MinValue = newValue;
                    break;
                
                case StatValueType.MaxValue:
                    MaxValue = newValue;
                    break;
                
                default:
                    throw new Exception($"No value of type : {statValueType.GetType().Name}");
            }
        }

        private void InitInternal(StatConfig config)
        {
            HasMinValue = config.HasMinValue;
            HasMaxValue = config.HasMaxValue;

            Value = config.InitValue;
            MinValue = config.MinValue;
            MaxValue = config.MaxValue;
        }

        public void Dispose()
        {
            OnStatChanged?.Dispose();
        }
    }
}