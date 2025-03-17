using System.Collections.Generic;
using Project.Scripts.App;
using UnityEngine.SceneManagement;

namespace Project.Scripts
{
    public class SceneLoader : ISceneLoader
    {
        private Dictionary<GameStateType, int> _scenes = new()
        {
            {GameStateType.SandBox, 1},
            {GameStateType.MainMenu, 2},
            {GameStateType.Hub, 3},
            {GameStateType.Church, 4}
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