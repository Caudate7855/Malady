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

        [Inject] protected GlobalFactory GlobalFactory;
        [Inject] protected IPanelManager PanelManager;
        [Inject] private InputController _inputController;

        private readonly Vector3 _playerPosition = new(0, 0, 0);
        private PlayerController _playerController;
        private CoreUpdater _coreUpdater;
        
        private async void Start()
        {
            Initialize();
         
            _playerController = await GlobalFactory.CreatePlayer(_playerPosition);

            _mainCamera.Initialize(_playerController);
            _inputController.Initialize(_playerController, PanelManager);
            
            await FinishLoading();

            PanelManager.LoadPanel<MainUIController>().Open();
        }
        
        private void OnEnable()
        {
            _coreUpdater = FindFirstObjectByType<CoreUpdater>();
            _coreUpdater.OnUpdatePerformed += _inputController.Update;
        }

        private void OnDisable()
        {
            _coreUpdater.OnUpdatePerformed -= _inputController.Update;
        }

        protected abstract void Initialize();


        private async UniTask FinishLoading()
        {
            var controller = PanelManager.LoadPanel<LoadingOverlayController>();
            var fader = PanelManager.LoadPanel<FaderController>();

            fader.Open();
            fader.FadeIn();

            await UniTask.Delay((int)FaderController.FadeDuration * 1000);

            controller.Close();
        }
    }
}