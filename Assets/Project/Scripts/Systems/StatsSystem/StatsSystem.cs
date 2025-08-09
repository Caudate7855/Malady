using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.FullSerializer;
using UnityEngine.Rendering;

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
            {new AttackSpeedStat()},
            
            {new MoveSpeedStat()},
            
            {new CritChanceStat()},
            {new CritDamageStat()},
            
            {new ArmorStat()},
            {new MagicResistStat()},
            
            {new SkeletonsCountStat()},
            {new MagicResistStat()},
        };

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
            //TODO: создать логику инициализации из бинарника сохранения
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
        
        public List<IStat> GetStats()
        {
            return Stats;
        }

        public StatBase GetStat<T>() where T : StatBase
        {
            for (int i = 0; i < Stats.Count; i++)
            {
                if (Stats[i].GetType() == typeof(T))
                {
                    return Stats[i] as StatBase;
                }
            }

            return null;
        }

        public void UpdateStat<T>(float newValue) where T : IStat
        {
            var stat = Stats.OfType<T>().FirstOrDefault();

            if (stat != null)
            {
                stat.Update(newValue);
            }
        }
    }
}