using System.Collections.Generic;
using Project.Scripts.App;

namespace Project.Scripts
{
    public interface ISceneLoader
    {
        public void LoadScene(GameStateType gameState);
    }
}