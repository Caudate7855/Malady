using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class MainMenuView : PanelBase
    {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;
        
        public Button StartGameButton => _startGameButton;
        public Button SettingsButton => _settingsButton;
        public Button ExitButton => _exitButton;
    }
}