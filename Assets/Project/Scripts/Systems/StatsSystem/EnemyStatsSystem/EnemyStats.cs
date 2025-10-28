using System.Collections.Generic;

namespace Project.Scripts
{
    public class EnemyStats : IStatSystem
    {
        public bool IsInitialized { get; set; }
        public List<IStat> GetStats()
        {
            throw new System.NotImplementedException();
        }

        public T GetStat<T>() where T : StatBase
        {
            throw new System.NotImplementedException();
        }

        public List<IStat> Stats { get; set; }
        public void UpdateStat<T>(float newValue) where T : IStat
        {
            throw new System.NotImplementedException();
        }

        public void DefaultInitialize()
        {
            throw new System.NotImplementedException();
        }

        public void InitializeFromSaves()
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            throw new System.NotImplementedException();
        }
    }
}