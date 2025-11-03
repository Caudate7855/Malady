using System.Collections.Generic;

namespace Project.Scripts
{
    public interface IStatSystem
    {
        public bool IsInitialized { get; set; }
        public List<IStat> Stats { get; set; }

        public void Initialize();
        public List<IStat> GetAllStats();
        public T GetStat<T>() where T : StatBase;
        public void UpdateStat<T>(float newValue) where T : IStat;
        public void ChangeStat<T>(float changeValue) where T : IStat;
    }
}