using Cysharp.Threading.Tasks;
using Itibsoft.PanelManager;
using Project.Scripts.App;
using Project.Scripts.Core;
using Project.Scripts.Core.Dungeon;
using Project.Scripts.Overlays;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class HubBoot : MonoBehaviour
    {
        private DiContainer _diContainer;
        
        [Inject] private IPanelManager _panelManager;

        [Inject] private GameDirector _gameDirector;
        [Inject] private IStatSystem _statSystem;

        [Inject] private DungeonFactory _dungeonFactory;
        [Inject] private PlayerFactory _playerFactory;
        [Inject] private EnemyFactory _enemyFactory;
        
        [Inject] private BookInteractable _book;

        private Dungeon _dungeon;
        private PlayerController _playerController;

        [Inject] private PlayerInputController _playerInputController;

        [SerializeField] private CameraFollower _mainCamera;

        private readonly Vector3 _enemyMeleePosition = new(1, 0, 0);
        private readonly Vector3 _enemyRangePosition = new(2, 0, 0);
        private readonly Vector3 _playerPosition = new(0, 0, 0);

        private async void Start()
        {
            await _dungeonFactory.Create<Dungeon>();

            _playerController = await _playerFactory.Create<PlayerController>(_playerPosition);
            _playerController.InitializeDependencies(_statSystem);

            _mainCamera.Initialize(_playerController);
            _playerInputController.Initialize(_playerController, _panelManager);

            _book = Instantiate(_book);

            await _enemyFactory.Create<EnemyMelee>(EnemyTypes.Melee, _enemyMeleePosition);
            await _enemyFactory.Create<EnemyRange>(EnemyTypes.Range, _enemyRangePosition);

            await FinishLoading();

            _panelManager.LoadPanel<MainUIController>().Open();
        }

        private void Update()
        {
            if (_playerInputController != null)
            {
                _playerInputController.Update();
            }
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