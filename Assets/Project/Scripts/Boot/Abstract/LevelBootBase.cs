using System;
using Cysharp.Threading.Tasks;
using Itibsoft.PanelManager;
using Project.Scripts.App;
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
            InitializeCoreUpdater();

            _coreUpdater.OnUpdatePerformed += _playerInputController.Update;
            
            Initialize();
            _playerController = await _playerFactory.Create(_playerPosition);

            _mainCamera.Initialize(_playerController);
            _playerInputController.Initialize(_playerController, _panelManager);


            //await CreateTestEnemies();
            await FinishLoading();

            _panelManager.LoadPanel<MainUIController>().Open();
        }

        private void OnDisable()
        {
            _coreUpdater.OnUpdatePerformed -= _playerInputController.Update;
        }

        protected abstract void Initialize();

        private void InitializeCoreUpdater()
        {
            _coreUpdater = FindObjectOfType<CoreUpdater>();
        }

        private async UniTask CreateTestEnemies()
        {
            await _enemyFactory.Create<EnemyMelee>(EnemyTypes.Melee, new Vector3(1, 0, 0));
            await _enemyFactory.Create<EnemyRange>(EnemyTypes.Range, new Vector3(2, 0, 0));
        }

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