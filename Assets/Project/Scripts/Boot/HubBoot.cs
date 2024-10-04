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
    public class HubBoot: MonoBehaviour
    {
        [Inject]private IPanelManager _panelManager;

        [Inject] private GameDirector _gameDirector;
        [Inject] private IStatSystem _statSystem;
        
        [Inject]private DungeonFactory _dungeonFactory;
        [Inject]private PlayerFactory _playerFactory;
        [Inject] private EnemyFactory _enemyFactory;
        
        private Dungeon _dungeon;
        private Player _player;
        
        [SerializeField] private CameraFollower _mainCamera;
        
        private readonly Vector3 _enemyMeleePosition = new(1,0,0);
        private readonly Vector3 _enemyRangePosition = new(2,0,0);
        private readonly Vector3 _playerPosition = new(0,0,0);

        private async void Start()
        {
            await _dungeonFactory.Create<Dungeon>();
            
            _player = await _playerFactory.Create<Player>(_playerPosition);
            _player.InitializeDependencies(_statSystem);
            
            _mainCamera.Initialize(_player);
            
            PlayerInputController playerInputController = new (_player, _panelManager);
            
            await _enemyFactory.Create<EnemyMelee>(EnemyTypes.Melee, _enemyMeleePosition);
            await _enemyFactory.Create<EnemyRange>(EnemyTypes.Range, _enemyRangePosition);

            _panelManager.LoadPanel<MainUIController>().Open();
            
            FinishLoading();
        }

        private async void FinishLoading()
        {
            var controller = _panelManager.LoadPanel<LoadingOverlayController>();
            var fader = _panelManager.LoadPanel<FaderController>();

            await UniTask.Delay((int)fader.FadinDuration * 1000);
            
            controller.Close();
        }
    }
}