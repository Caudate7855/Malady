using System.Threading.Tasks;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts
{
    public class ChurchFactory
    {
        private const string CHURCH_ADDRESS = "Church";
        
        private readonly Vector3 _position = new(0, 0, 0);
        private readonly IAssetLoader _assetLoader;
        
        public ChurchFactory(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }
        
        public async Task<T> Create<T>() where T : Object, ICustomInitializable
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<Object>(CHURCH_ADDRESS);
            
            var gameObject = Object.Instantiate(prefab, _position, Quaternion.identity);
            var component = gameObject.GetComponent<T>();
            
            component.Initialize();
            return component;
        }
    }
}