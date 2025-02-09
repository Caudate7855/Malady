using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class PlayerFactory
    {
        private const string PLAYER_ADDRESS = "Player";

        [Inject]private DiContainer _diContainer;
        private readonly IAssetLoader _assetLoader;

        public PlayerFactory(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }
        
        public async Task<PlayerController> Create(Vector3 spawnPosition)
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<PlayerController>(PLAYER_ADDRESS);
            
            var gameObject = _diContainer.InstantiatePrefabForComponent<PlayerController>(prefab);
            var component = gameObject.GetComponent<PlayerController>();
            
            component.Initialize();
            
            return component;
        }
    }
}