using Project.Scripts.Core.Dungeon;
using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class ProjectEntryPoint: MonoBehaviour
    {
        private DungeonFactory _dungeonFactory;
        private Dungeon _dungeon;
        
        [Inject]
        public void Construct(DungeonFactory dungeonFactory)
        {
            _dungeonFactory = dungeonFactory;
        }
        
        private async void Start()
        {
            _dungeon = await _dungeonFactory.Create<Dungeon>();
        }
    }
}