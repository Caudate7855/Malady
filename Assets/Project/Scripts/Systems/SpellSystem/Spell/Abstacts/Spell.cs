using Cysharp.Threading.Tasks;
using Project.Scripts.Core;
using Zenject;

namespace Project.Scripts
{
    public abstract class SpellBase
    {
        public string ID;
        [Inject] protected SummonSystem SummonSystem;
        [Inject] protected MouseController MouseController;
        [Inject] protected PlayerStats PlayerStats;

        protected bool IsInitialized;

        public abstract void Initialize();

        public abstract void Cast();
        
        public abstract void Clear();
    }
}