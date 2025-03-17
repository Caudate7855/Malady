using System.Threading.Tasks;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using UnityEngine;

namespace Project.Scripts
{
    public class SandBoxFactory
    {
        private readonly Vector3 _position = new(0, 0, 0);

        private readonly IAssetLoader _assetLoader;
        
        public SandBoxFactory(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }
        
        public async Task<T> Create<T>() where T : Object, ICustomInitializable
        {
            var gameObject = Object.Instantiate(new GameObject(), _position, Quaternion.identity);
            var component = gameObject.GetComponent<T>();
            
            component.Initialize();
            return component;
        }
    }
}