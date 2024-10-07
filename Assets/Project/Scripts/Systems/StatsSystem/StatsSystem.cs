using System.Collections.Generic;

namespace Project.Scripts
{
    public class StatsSystem : IStatSystem
    {
        public bool IsInitialized { get; set; }
        public List<IStat> Stats { get; set; } = new()
        {
            {new HpStat()},
            {new EssenceStat()},
            
            {new DamageStat()},
            { new AttackSpeedStat()},
            
            {new MoveSpeedStat()},
            
            {new CritChanceStat()},
            {new CritDamageStat()},
            
            {new ArmorStat()},
            {new MagicResistStat()}
        };

        public List<IStat> GetStats()
        {
            return Stats;
        }

        public void DefaultInitialize()
        {
            for (int i = 0, count = Stats.Count; i < count; i++)
            {
                Stats[i].InitializeValuesDefault();
            }

            IsInitialized = true;
        }

        public void InitializeFromSaves()
        {
            //TODO: создать логику инициализации из JSON сохранения
        }

        public void Initialize()
        {
            if (IsInitialized == false)
            {
                DefaultInitialize();
            }
            else
            {
                InitializeFromSaves();
            }
        }
    }
}