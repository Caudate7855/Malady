using System.Collections.Generic;

namespace Project.Scripts
{
    public class DungeonGenerationSystem
    {
        private DungeonGenerationSettings _settings;
        
        private DungeonGenerator _dungeonGenerator;
        private List<DungeonRoom> _dungeonRooms;
        
        public DungeonGenerationSystem(DungeonGenerator dungeonGenerator)
        {
            _dungeonGenerator = dungeonGenerator;
        }

        public void SetDungeonSettings(DungeonGenerationSettings dungeonGenerationSettings)
        {
            _settings = dungeonGenerationSettings;
        }
        
        public void SetRooms(List<DungeonRoom> dungeonRooms)
        {
            _dungeonRooms = dungeonRooms;
        }
        
        public void GenerateDungeon()
        {
            
        }
    }
}