using Cysharp.Threading.Tasks;
using Itibsoft.PanelManager;
using Project.Scripts.Core;
using Project.Scripts.Overlays.Inventory;
using Zenject;

namespace Project.Scripts
{
    public abstract class SpellBase
    {
        public string ID;
        [Inject] protected SummonSystem SummonSystem;
        [Inject] protected MouseController MouseController;
        [Inject] protected PlayerStats PlayerStats;
        [Inject] protected IPanelManager PanelManager;
        protected StatType Type;

        protected InventoryController InventoryController;

        protected bool IsInitialized;

        public abstract void Initialize();

        public abstract void Cast();
        
        public abstract void Clear();
    }
}