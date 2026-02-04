using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts
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

        private void OnStartGameButtonClicked()
        {
            _main.ChangeState(SceneType.Hub);
            Close();
        }

        private void OnSettingsButtonClicked()
        {
            
        }

        private void OnExitButtonClicked()
        {
        
        }
    }
}