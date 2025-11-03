using System.Collections.Generic;
using System.Linq;

namespace Project.Scripts
{
    public abstract class StatSystemBase :  IStatSystem
    {
        public bool IsInitialized { get; set; }
        public virtual List<IStat> Stats { get; set; }
        
        public virtual void Initialize()
        {
            for (int i = 0, count = Stats.Count; i < count; i++)
            {
                Stats[i].InitializeValuesDefault();
            }

            IsInitialized = true;
        }
        
        public virtual List<IStat> GetAllStats()
        {
            return Stats;
        }

        public virtual T GetStat<T>() where T : StatBase
        {
            for (int i = 0; i < Stats.Count; i++)
            {
                if (Stats[i].GetType() == typeof(T))
                {
                    return Stats[i] as T;
                }
            }

            return null;
        }

        public virtual void UpdateStat<T>(float newValue) where T : IStat
        {
            var stat = Stats.OfType<T>().FirstOrDefault();

            if (stat != null)
            {
                stat.Update(newValue);
            }
        }

        public virtual void ChangeStat<T>(float changeValue) where T : IStat
        {
            var stat = Stats.OfType<T>().FirstOrDefault();

            if (stat != null)
            {
                stat.Value += changeValue;
                stat.Update(changeValue);
            }
        }
    }
}