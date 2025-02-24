using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class CorpseFactory
    {
        private const string CORPSE_ADDRESS = "Corpse";

        private readonly IAssetLoader _assetLoader;

        public CorpseFactory(IAssetLoader assetLoader)
        {
            _assetLoader = assetLoader;
        }

        public async Task<T> Create<T>(bool hasSoul, bool hasBlood, bool hasFlesh, bool hasBones, Vector3 spawnPosition)
            where T : Corpse, ICustomInitializable
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<Corpse>(CORPSE_ADDRESS);
            var gameObject = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            var component = gameObject.GetComponent<T>();

            component.Initialize();

            return component;
        }
    }
}