using Itibsoft.PanelManager;
using Project.Scripts.Core;
using Project.Scripts.Core.Dungeon;
using Project.Scripts.UI.Overlays;
using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class ProjectEntryPoint: GameTypeBase
    {
        private IPanelManager _panelManager;

        [Inject] private GameDirector _gameDirector;
        
        private DungeonFactory _dungeonFactory;
        private PlayerFactory _playerFactory;
        private EnemyFactory _enemyFactory;
        private Dungeon _dungeon;
        private Player _player;
        
        [SerializeField] private CameraFollower _mainCamera;
        
        private readonly Vector3 _enemyMeleePosition = new(1,0,0);
        private readonly Vector3 _enemyRangePosition = new(2,0,0);
        private readonly Vector3 _playerPosition = new(0,0,0);

        [Inject]
        public void Construct(DungeonFactory dungeonFactory, PlayerFactory playerFactory, 
            EnemyFactory enemyFactory, IPanelManager panelManager)
        {
            _dungeonFactory = dungeonFactory;
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
            _panelManager = panelManager;
        }

        private async void Start()
        {
            await _dungeonFactory.Create<Dungeon>();
            
            _player = await _playerFactory.Create<Player>(_playerPosition);
            _mainCamera.Initialize(_player);
            
            PlayerInputController playerInputController = new (_player, _panelManager);
            
            await _enemyFactory.Create<EnemyMelee>(EnemyTypes.Melee, _enemyMeleePosition);
            await _enemyFactory.Create<EnemyRange>(EnemyTypes.Range, _enemyRangePosition);

            FinishLoading();
        }

        private void FinishLoading()
        {
            _panelManager.LoadPanel<LoadingOverlayController>().Close();
        }
    }
}