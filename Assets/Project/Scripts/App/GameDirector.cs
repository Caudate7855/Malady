using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Scripts.App
{
    public class GameDirector : MonoBehaviour
    {
        private static  readonly Dictionary<GameType, int> _gameTypeScenes = new()
        {
            {GameType.MainMenu, 0},
            {GameType.Core, 1}
        };

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            Enter(GameType.MainMenu);
        }

        public static void Enter(GameType gameType)
        {
            if (_gameTypeScenes.TryGetValue(gameType, out var sceneIndex))
            {
                SceneManager.LoadScene(sceneIndex);
            }
        }
    }
}