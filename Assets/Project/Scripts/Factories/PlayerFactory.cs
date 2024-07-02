using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class PlayerFactory
    {
        private const string PLAYER_ADDRESS = "Player";
        private readonly IAssetLoader _assetLoader;
        
        public PlayerFactory(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }
        
        public async Task<T> Create<T>(Vector3 spawnPosition) where T : Object, ICustomInitializable
        {
            var prefab = await _assetLoader.Load<Object>(PLAYER_ADDRESS);
            
            var gameObject = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            var component = gameObject.GetComponent<T>();
            
            component.Initialize();
            
            return component;
        }
    }
}