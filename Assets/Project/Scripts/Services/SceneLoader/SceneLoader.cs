using System.Collections.Generic;
using Project.Scripts.App;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class SceneLoader : ISceneLoader
    {
        private Dictionary<SceneType, int> _scenes = new()
        {
            {SceneType.SandBox, 1},
            {SceneType.DungeonGeneration, 2},
            {SceneType.MainMenu, 3},
            {SceneType.Hub, 4},
            {SceneType.Church, 5}
        };
        
        public void LoadScene(SceneType scene)
        {
            if (_scenes.TryGetValue(scene, out var sceneIndex))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}