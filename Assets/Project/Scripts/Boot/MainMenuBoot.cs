using Itibsoft.PanelManager;
using Project.Scripts.Overlays.MainMenu;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class MainMenuBoot : MonoBehaviour
    {
        [Inject] private IPanelManager _panelManager;
        
        private void Start()
        {
            var panel = _panelManager.LoadPanel<MainMenuController>();
            panel.Open();
        }
    }
}