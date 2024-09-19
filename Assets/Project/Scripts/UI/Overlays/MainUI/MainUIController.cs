using Itibsoft.PanelManager;

namespace Project.Scripts.UI.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        protected override void Initialize()
        {
            
        }
    }
}