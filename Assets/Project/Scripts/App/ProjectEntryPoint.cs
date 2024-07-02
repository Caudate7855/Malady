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
        
        private Dungeon _dungeon;
        private Player _player;

        [Inject]
        public void Construct(DungeonFactory dungeonFactory, PlayerFactory playerFactory)
        {
            _dungeonFactory = dungeonFactory;
            _playerFactory = playerFactory;
        }
        
        private async void Start()
        {
            _dungeon = await _dungeonFactory.Create<Dungeon>();
            _player = await _playerFactory.Create<Player>();
        }
    }
}