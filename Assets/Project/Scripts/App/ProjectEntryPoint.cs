using System;
using Itibsoft.PanelManager;
using Project.Scripts.Core;
using Project.Scripts.Core.Dungeon;
using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class ProjectEntryPoint: GameTypeBase
    {
        private IPanelManager _panelManager;
        
        private DungeonFactory _dungeonFactory;
        private PlayerFactory _playerFactory;
        private EnemyFactory _enemyFactory;
        private Dungeon _dungeon;
        private Player _player;
        
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
            
            PlayerInputController playerInputController = new (_player, _panelManager);
            
            await _enemyFactory.Create<EnemyMelee>(EnemyTypes.Melee, _enemyMeleePosition);
            await _enemyFactory.Create<EnemyRange>(EnemyTypes.Range, _enemyRangePosition);
        }
    }
}