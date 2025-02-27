using Itibsoft.PanelManager;
using Project.Scripts.App;
using Zenject;

namespace Project.Scripts.Overlays.MainMenu
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainMenuView")]
    public class MainMenuController : PanelControllerBase<MainMenuView>
    {
        [Inject] private Main _main;

        protected override void Initialize()
        {
            Panel.StartGameButton.onClick.AddListener(OnStartGameButtonClicked);
            Panel.SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
            Panel.ExitButton.onClick.AddListener(OnExitButtonClicked);
        }

        #region ButtonsMethods
        
        private void OnStartGameButtonClicked()
        {
            _main.ChangeState(GameStateType.Hub);
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