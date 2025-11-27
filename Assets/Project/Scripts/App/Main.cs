using Itibsoft.PanelManager;
using Project.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private bool _isSandBox;
        [SerializeField] private bool _isDungeonGenerationTest;
        
        [Inject] private IPanelManager _panelManager;
        [Inject] private ISceneLoader _sceneLoader;

        private LoadingOverlayController _loadingOverlayController;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);

            _loadingOverlayController = _panelManager.LoadPanel<LoadingOverlayController>();
        }

        private void Start()
        {
            if (_isSandBox)
            {
                ChangeState(SceneType.SandBox);
                return;
            }
            
            if (_isDungeonGenerationTest)
            {
                ChangeState(SceneType.DungeonGeneration);
                return;
            }
            
            ShowLoadingScreen();
            ChangeState(SceneType.MainMenu);
        }

        private void ShowLoadingScreen()
        {
            _loadingOverlayController.Open();
        }

        public void ChangeState(SceneType sceneType)
        {
            ShowLoadingScreen();
            
            _sceneLoader.LoadScene(sceneType);
        }
    }
}