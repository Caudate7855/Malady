using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Overlays.MainMenu
{
    public class MainMenuView : PanelBase
    {
        [SerializeField] private Button _startGameButton;
        
        public Button StartGameButton => _startGameButton;
    }
}