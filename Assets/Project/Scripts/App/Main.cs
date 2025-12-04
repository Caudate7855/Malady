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
            
            _loadingOverlayController.Open();
            ChangeState(SceneType.MainMenu);
        }

        public void ChangeState(SceneType sceneType)
        {
            _loadingOverlayController.Open();
            _sceneLoader.LoadScene(sceneType);
        }
    }
}