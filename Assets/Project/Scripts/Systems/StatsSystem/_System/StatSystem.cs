using System;
using System.Collections.Generic;
using Zenject;

namespace Project.Scripts
{
    public class StatSystem : IInitializable, IDisposable
    {
        public List<IStat> PlayerStats { get; private set; } = new();

        private readonly StatsConfig _statsConfig;

        public StatSystem(StatsConfig statsConfig)
        {
            _statsConfig = statsConfig;
        }

        public void Initialize()
        {
            for (int i = 0, count = _statsConfig.StatsList.Count; i < count; i++)
            {
                var statConfig = _statsConfig.StatsList[i];
                var statType = statConfig.Type;

                var newStat = (IStat)Activator.CreateInstance(statType.GetType());

                if (newStat != null)
                {
                    newStat.Init(statConfig);
                    PlayerStats.Add(newStat);
                }
                else
                {
                    throw new Exception($"Failed to create instance of {statType.GetType().Name}");
                }
            }
        }

        public IStat GetStat<T>() where T : IStat
        {
            for (int i = 0, count = PlayerStats.Count; i < count; i++)
            {
                if (PlayerStats[i] is T stat)
                {
                    return stat;
                }
            }

            throw new Exception($"Can't find Stat of type {typeof(T)}");
        }

        public void Dispose()
        {
            for (int i = 0, count = PlayerStats.Count; i < count; i++)
            {
                PlayerStats[i].Dispose();
            }
            
            PlayerStats.Clear();
        }
    }
}