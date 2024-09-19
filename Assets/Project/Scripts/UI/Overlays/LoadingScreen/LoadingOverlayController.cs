using Itibsoft.PanelManager;

namespace Project.Scripts.UI.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "LoadingScreen")]
    public class LoadingOverlayController : PanelControllerBase<LoadingOverlayView>
    {
        protected override void Initialize()
        {
            
        }
    }
}