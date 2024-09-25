using System.Collections;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Overlays;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Project.Scripts.App
{
    public class GameDirector : MonoBehaviour
    {
        [Inject] private IPanelManager _panelManager;
        
        private LoadingOverlayController _loadingOverlayController;
        
        private static  readonly Dictionary<GameType, int> _gameTypeScenes = new()
        {
            {GameType.MainMenu, 0},
            {GameType.Core, 1}
        };

        private void Awake()
        {
            _loadingOverlayController = _panelManager.LoadPanel<LoadingOverlayController>();
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            ShowLoadingScreen();
            Enter(GameType.MainMenu);
        }
        
        public void ShowLoadingScreen()
        {
            _loadingOverlayController.Open();
        }

        public void CloseLoadingScreen()
        {
            _loadingOverlayController.Close();
        }

        public static void Enter(GameType gameType)
        {
            if (_gameTypeScenes.TryGetValue(gameType, out var sceneIndex))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}