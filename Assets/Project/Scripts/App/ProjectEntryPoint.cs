using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class ProjectEntryPoint: MonoBehaviour
    {
        private const string DUNGEON_PATH = "Dungeon";
        private const string PLAYER_PATH = "Player";
        
        [Inject] private PlayerFactory _playerFactory;
        [Inject] private DungeonFactory _dungeonFactory;

        private Dungeon _dungeon;

        private async void Start()
        {
            _dungeon = _dungeonFactory.Create(DUNGEON_PATH).Result;
            await _playerFactory.Create(PLAYER_PATH);
        }
    }
}