using Project.Scripts.Core;
using Project.Scripts.Core.Dungeon;
using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class ProjectEntryPoint: MonoBehaviour
    {
        private DungeonFactory _dungeonFactory;
        private PlayerFactory _playerFactory;
        private EnemyFactory _enemyFactory;
        
        private Dungeon _dungeon;
        private Player _player;
        private EnemyBase _enemy;

        private readonly Vector3 _enemyPosition = new(1,0,0);
        private readonly Vector3 _playerPosition = new(0,0,0);

        [Inject]
        public void Construct(DungeonFactory dungeonFactory, PlayerFactory playerFactory, EnemyFactory enemyFactory)
        {
            _dungeonFactory = dungeonFactory;
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
        }
        
        private async void Start()
        {
            _dungeon = await _dungeonFactory.Create<Dungeon>();
            _player = await _playerFactory.Create<Player>(_playerPosition);
            _enemy = await _enemyFactory.Create<Enemy>(_player, EnemyTypes.Melee, _enemyPosition);
        }
    }
}