using Itibsoft.PanelManager;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        public void OnPlayerSpellButtonClicked(int index)
        {
            
        }
    }
}