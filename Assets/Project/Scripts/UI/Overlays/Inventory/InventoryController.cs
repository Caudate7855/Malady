using Itibsoft.PanelManager;

namespace Project.Scripts.UI.Overlays.Inventory
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "InventoryView")]
    public class InventoryController : PanelControllerBase<InventoryView>
    {
        protected override void Initialize()
        {
            
        }
    }
}