namespace Project.Scripts
{
    public class ArmorStat : StatBase
    {
        public override string Name { get; set; } = "Armor";
        public override StatType Type { get; set; } = StatType.Armor;
        public override float Value { get; set; }
        public override float MaxValue { get; set; }
        public override float MinValue { get; set; }
        public override bool HasMaxValue { get; set; }
        
        public override void InitializeValuesDefault()
        {
            InitializeValues(5);
        }
    }
}