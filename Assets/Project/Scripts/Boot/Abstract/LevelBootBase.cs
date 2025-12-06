using System;
using Cysharp.Threading.Tasks;
using Itibsoft.PanelManager;
using Project.Scripts.Services;
using R3;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public abstract class LevelBootBase : MonoBehaviour, IDisposable, IInitializable
    {
        [SerializeField] private CameraFollower _mainCamera;

        [Inject] protected GlobalFactory GlobalFactory;
        [Inject] protected IPanelManager PanelManager;
        [Inject] private InputController _inputController;
        [Inject] private PlayerController _playerController;

        private readonly Vector3 _playerPosition = new(0, 0, 0);
        private CoreUpdater _coreUpdater;
        private CompositeDisposable _compositeDisposable = new();

        public abstract void Initialize();

        private async void Start()
        {
            Initialize();
         
            _playerController.gameObject.transform.position = _playerPosition;

            await FinishLoading();

            PanelManager.LoadPanel<MainUIController>().Open();
        }

        private void OnEnable()
        {
            _coreUpdater = FindFirstObjectByType<CoreUpdater>();
            
            _coreUpdater.OnUpdatePerformed
                .Subscribe(_ =>_inputController.Update())
                .AddTo(_compositeDisposable);
        }

        private async UniTask FinishLoading()
        {
            var controller = PanelManager.LoadPanel<LoadingOverlayController>();
            var fader = PanelManager.LoadPanel<FaderController>();

            fader.Open();
            fader.FadeIn();

            await UniTask.Delay((int)FaderController.FadeDuration * 1000);

            controller.Close();
        }

        public virtual void Dispose()
        {
            _compositeDisposable.Dispose();   
        }
    }
}