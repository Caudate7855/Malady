using System.Threading.Tasks;
using Project.Scripts.Interfaces;
using Unity.VisualScripting;
using UnityEngine;

namespace Project.Scripts
{
    public class DungeonFactory
    {
        private const string DUNGEON_ADDRESS = "Dungeon";
        private readonly Vector3 _position = new Vector3(0, 0, 0);
        
        public async Task<T> Create<T>() where T : Object, ICustomInitializable
        {
            var prefab = await AssetLoader.Load<Object>(DUNGEON_ADDRESS);
            
            var gameObject = Object.Instantiate(prefab, _position, Quaternion.identity);
            var component = gameObject.GetComponent<T>();
            
            
            component.Initialize();
            
            return component;
        }
    }
}