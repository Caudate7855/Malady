using System;
using System.Collections.Generic;

namespace Project.Scripts
{
    public class StatsSystem
    {
        private Dictionary<StatType, IStat> _stats = new()
        {
            { StatType.HP, new Stat()},
            { StatType.Essence, new Stat()},
            { StatType.Damage, new Stat()},
            { StatType.AttackSpeed, new Stat()},
            { StatType.MoveSpeed, new Stat()},
            { StatType.CritChance, new Stat()},
            { StatType.CritDamage, new Stat()}
        };

        public void DefaultInitialize()
        {
            foreach (var statPair in _stats)
            {
                var statType = statPair.Key;
                var stat = statPair.Value;

                switch (statType)
                {
                    case StatType.HP:
                        stat.Initialize(100, 100);
                        break;

                    case StatType.Essence:
                        stat.Initialize(100, 100);
                        break;

                    case StatType.Damage:
                        stat.Initialize(10);
                        break;

                    case StatType.AttackSpeed:
                        stat.Initialize(100, 300);
                        break;

                    case StatType.MoveSpeed:
                        stat.Initialize(100, 200);
                        break;

                    case StatType.CritChance:
                        stat.Initialize(10, 100);
                        break;

                    case StatType.CritDamage:
                        stat.Initialize(0, 100);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Initialize()
        {
            
        }
    }
}