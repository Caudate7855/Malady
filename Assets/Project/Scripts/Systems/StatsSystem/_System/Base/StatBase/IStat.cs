using System;
using R3;

namespace Project.Scripts
{
    public interface IStat : IDisposable
    {
        public StatType StatType { get; }

        public bool HasMinValue { get;}
        public bool HasMaxValue { get;}

        public float Value { get;}
        public float MinValue { get;}
        public float MaxValue { get;}

        ReactiveCommand<IStat> OnStatChanged { get;}

        void Init(StatConfig statConfig);
    }
}