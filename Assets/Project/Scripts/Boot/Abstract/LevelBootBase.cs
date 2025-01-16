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
        [Inject] private IPanelManager _panelManager;
        [Inject] private IStatSystem _statSystem;

        [Inject] private PlayerFactory _playerFactory;
        [Inject] private EnemyFactory _enemyFactory;

        private PlayerController _playerController;

        [Inject] private PlayerInputController _playerInputController;

        [SerializeField] private CameraFollower _mainCamera;

        private readonly Vector3 _playerPosition = new(0, 0, 0);

        private CoreUpdater _coreUpdater;
        

        private async void Start()
        {
            InitializeCoreUpdater();

            _coreUpdater.OnUpdatePerformed += _playerInputController.Update;

            Initialize();
            _playerController = await _playerFactory.Create<PlayerController>(_playerPosition);
            _playerController.InitializeDependencies(_statSystem);

            _mainCamera.Initialize(_playerController);
            _playerInputController.Initialize(_playerController, _panelManager);


            //await CreateTestEnemies();
            await FinishLoading();

            _panelManager.LoadPanel<MainUIController>().Open();
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