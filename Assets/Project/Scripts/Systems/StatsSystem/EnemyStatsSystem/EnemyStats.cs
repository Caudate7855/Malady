using System.Collections.Generic;

namespace Project.Scripts
{
    public class EnemyStats : StatSystemBase
    {
        public override List<IStat> Stats { get; set; } = new()
        {
            {new HpStat()},
            {new DamageStat()},
            {new AttackSpeedStat()},
            {new MoveSpeedStat()},
            {new ArmorStat()},
            {new MagicResistStat()}
        };
    }
}