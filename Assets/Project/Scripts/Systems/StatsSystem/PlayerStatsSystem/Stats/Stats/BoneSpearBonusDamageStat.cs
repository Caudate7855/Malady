using JetBrains.Annotations;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class BoneSpearBonusDamageStat : StatBase
    {
        public override string Name { get; set; } = "Blood lance bonus damage";
        public override StatType Type { get; set; } = StatType.BloodLanceDamageStat;
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