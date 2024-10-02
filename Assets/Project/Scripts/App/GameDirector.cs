using Itibsoft.PanelManager;
using Project.Scripts.Overlays;
using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class GameDirector : MonoBehaviour
    {
        [Inject] private IPanelManager _panelManager;
        [Inject] private ISceneLoader _sceneLoader;
        
        private LoadingOverlayController _loadingOverlayController;

        private void Awake()
        {
            _loadingOverlayController = _panelManager.LoadPanel<LoadingOverlayController>();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            ShowLoadingScreen();
            Enter(GameStateType.MainMenu);
        }
        
        public void ShowLoadingScreen()
        {
            _loadingOverlayController.Open();
        }

        public void CloseLoadingScreen()
        {
            _loadingOverlayController.Close();
        }

        public void Enter(GameStateType gameStateType)
        {
            _sceneLoader.LoadScene(gameStateType);
        }
    }
}