namespace Project.Scripts
{
    public class DungeonGenerationBoot : LevelBootBase
    {
        private DungeonGenerationSceneController _dungeonGenerationSceneController;
        
        public override async void Initialize()
        {
            _dungeonGenerationSceneController =  await GlobalFactory.CreateAndInitializeAsync<DungeonGenerationSceneController>("DungeonGeneration");
        }
    }
}