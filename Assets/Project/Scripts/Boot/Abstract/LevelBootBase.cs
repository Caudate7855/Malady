using Cysharp.Threading.Tasks;
using Itibsoft.PanelManager;
using Project.Scripts.Core;
using Project.Scripts.Overlays;
using Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public abstract class LevelBootBase : MonoBehaviour
    {
        [SerializeField] private CameraFollower _mainCamera;
        
        [Inject] private IPanelManager _panelManager;
        [Inject] private PlayerFactory _playerFactory;
        [Inject] private EnemyFactory _enemyFactory;
        [Inject] private PlayerInputController _playerInputController;

        private readonly Vector3 _playerPosition = new(0, 0, 0);
        private PlayerController _playerController;
        private CoreUpdater _coreUpdater;
        
        private async void Start()
        {
            Initialize();
         
            _playerController = await _playerFactory.Create(_playerPosition);

            _mainCamera.Initialize(_playerController);
            _playerInputController.Initialize(_playerController, _panelManager);
            
            await FinishLoading();

            _panelManager.LoadPanel<MainUIController>().Open();
        }
        
        private void OnEnable()
        {
            _coreUpdater = FindObjectOfType<CoreUpdater>();
            _coreUpdater.OnUpdatePerformed += _playerInputController.Update;
        }

        private void OnDisable()
        {
            _coreUpdater.OnUpdatePerformed -= _playerInputController.Update;
        }

        protected abstract void Initialize();


        private async UniTask FinishLoading()
        {
            var controller = _panelManager.LoadPanel<LoadingOverlayController>();
            var fader = _panelManager.LoadPanel<FaderController>();

            fader.Open();
            fader.FadeIn();

            await UniTask.Delay((int)FaderController.FadeDuration * 1000);

            controller.Close();
        }
    }
}