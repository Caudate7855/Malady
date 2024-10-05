using System;
using System.Collections.Generic;

namespace Project.Scripts
{
    public interface IStatSystem
    {
        public bool IsInitialized { get; set; }
        public List<IStat> GetStats();
        public Dictionary<StatType, IStat> Stats { get; set; }

        public void DefaultInitialize();

        public void Initialize();
    }
}