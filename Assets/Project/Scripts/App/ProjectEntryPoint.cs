using Project.Scripts.Core.Dungeon;
using UnityEngine;

namespace Project.Scripts.App
{
    public class ProjectEntryPoint: MonoBehaviour
    {
        private readonly DungeonFactory _dungeonFactory = new();
        
        private Dungeon _dungeon;
        
        private async void Start()
        {
            _dungeon = await _dungeonFactory.Create<Dungeon>();
        }
    }
}