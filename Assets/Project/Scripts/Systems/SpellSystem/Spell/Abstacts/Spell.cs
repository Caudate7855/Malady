using Zenject;

namespace Project.Scripts
{
    public abstract class SpellBase
    {
        public string ID;
        [Inject] protected SummonSystem SummonSystem;
        [Inject] protected MouseController MouseController;

        protected bool IsInitialized;

        public abstract void Initialize();

        public abstract void Cast();
    }
}