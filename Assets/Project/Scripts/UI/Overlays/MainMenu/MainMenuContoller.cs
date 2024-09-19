using Itibsoft.PanelManager;
using Project.Scripts.App;

namespace Project.Scripts.UI.Overlays.MainMenu
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainMenuView")]
    public class MainMenuController : PanelControllerBase<MainMenuView>
    {
        protected override void Initialize()
        {
            Panel.StartGameButton.onClick.AddListener(OnStartGameButtonClicked);
            Panel.SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
            Panel.ExitButton.onClick.AddListener(OnExitButtonClicked);
        }

        #region ButtonsMethods
        
        private void OnStartGameButtonClicked()
        {
            GameDirector.Enter(GameType.Core);
            Close();
        }

        private void OnSettingsButtonClicked()
        {
            
        }

        private void OnExitButtonClicked()
        {
            
        }

        #endregion
    }
}