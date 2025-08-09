using System;
using System.Collections.Generic;

namespace Project.Scripts
{
    public interface IStatSystem
    {
        public bool IsInitialized { get; set; }
        public List<IStat> GetStats();
        public List<IStat> Stats { get; set; }

        public void UpdateStat<T>(float newValue) where T : IStat;

        public void DefaultInitialize();
        public void InitializeFromSaves();

        public void Initialize();
    }
}