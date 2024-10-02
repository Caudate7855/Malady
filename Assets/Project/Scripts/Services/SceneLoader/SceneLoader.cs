using System.Collections.Generic;
using Project.Scripts.App;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class SceneLoader : ISceneLoader
    {
        private Dictionary<GameStateType, int> _scenes = new()
        {
            {GameStateType.MainMenu, 0},
            {GameStateType.Hub, 1}
        };
        
        public void LoadScene(GameStateType gameState)
        {
            if (_scenes.TryGetValue(gameState, out var sceneIndex))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}