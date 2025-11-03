using System.Collections.Generic;
using System.Linq;

namespace Project.Scripts
{
    public class PlayerStats : StatSystemBase
    {
        public override List<IStat> Stats { get; set; } = new()
        {
            {new HpStat()},
            {new EssenceStat()},
            
            {new DamageStat()},
            {new AttackSpeedStat()},
            
            {new CritChanceStat()},
            {new CritDamageStat()},
            
            {new MoveSpeedStat()},
            
            {new ArmorStat()},
            {new MagicResistStat()},
            
            {new SkeletonWarriorsCountStat()},
            {new SkeletonArchersCountStat()},
            {new SkeletonMagesCountStat()},
            
            {new BloodSpearBonusDamageStat()}
        };
    }
}