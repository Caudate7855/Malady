using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class HubFactory
    {
        private const string HUB_ADDRESS = "Hub";
        
        private readonly Vector3 _position = new(0, 0, 0);

        private readonly IAssetLoader _assetLoader;
        
        public HubFactory(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }
        
        public async Task<T> Create<T>() where T : Object, ICustomInitializable
        {
            var prefab = await _assetLoader.Load<Object>(HUB_ADDRESS);
            
            var gameObject = Object.Instantiate(prefab, _position, Quaternion.identity);
            var component = gameObject.GetComponent<T>();
            
            component.Initialize();

            _assetLoader.CashedObject = null;
            
            return component;
        }
    }
}