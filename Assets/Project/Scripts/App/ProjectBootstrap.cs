using UnityEngine;
using Zenject;

namespace Project.Scripts.App
{
    public class ProjectBootstrap : MonoBehaviour
    {
        [Inject] private GameDirector _gameDirector;

        private void Awake()
        {
            //Instantiate(_gameDirector);
        }
    }
}