namespace Project.Scripts
{
    public class DungeonGenerationBoot : LevelBootBase
    {
        private DungeonGenerationSceneController _dungeonGenerationSceneController;
        
        protected override async void Initialize()
        {
            _dungeonGenerationSceneController =  await GlobalFactory.CreateAndInitializeAsync<DungeonGenerationSceneController>("DungeonGeneration");
        }
    }
}