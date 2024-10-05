using System;
using System.Collections.Generic;

namespace Project.Scripts
{
    public class StatsSystem : IStatSystem
    {
        public bool IsInitialized { get; set; }
        public Dictionary<StatType, IStat> Stats { get; set; } = new()
        {
            { StatType.HP, new HpStat()},
            { StatType.Essence, new EssenceStat()},
            { StatType.Damage, new DamageStat()},
            { StatType.AttackSpeed, new AttackSpeedStat()},
            { StatType.MoveSpeed, new MoveSpeedStat()},
            { StatType.CritChance, new CritChanceStat()},
            { StatType.CritDamage, new CritDamageStat()}
        };

        public List<IStat> GetStats()
        {
            var statsList = new List<IStat>();

            foreach (var stat in Stats)
            {
                statsList.Add(stat.Value);
            }

            return statsList;
        }

        public void DefaultInitialize()
        {
            foreach (var statPair in Stats)
            {
                var statType = statPair.Key;
                var stat = statPair.Value;

                switch (statType)
                {
                    case StatType.HP:
                        stat.InitializeValues(100, 100);
                        break;

                    case StatType.Essence:
                        stat.InitializeValues(100, 100);
                        break;

                    case StatType.Damage:
                        stat.InitializeValues(10);
                        break;

                    case StatType.AttackSpeed:
                        stat.InitializeValues(100, 300);
                        break;

                    case StatType.MoveSpeed:
                        stat.InitializeValues(100, 200);
                        break;

                    case StatType.CritChance:
                        stat.InitializeValues(10, 100);
                        break;

                    case StatType.CritDamage:
                        stat.InitializeValues(0, 100);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            IsInitialized = true;
        }

        public void Initialize()
        {
            if (IsInitialized == false)
            {
                DefaultInitialize();
            }
            else
            {
                //TODO: создать логику инициализации из JSON сохранения
            }
        }
    }
}