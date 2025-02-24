using System.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Core;
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
        
        public async Task<Corpse> CreateCustomCorpse(bool hasSoul, bool hasBlood, bool hasFlesh, bool hasBones, Vector3 spawnPosition)
        {
            var corpseComponent = await CreateDefaultCorpse(spawnPosition);

            if (!hasSoul)
            {
                corpseComponent.RemoveResource(ResourceType.Soul);
            }
            
            if (!hasBlood)
            {
                corpseComponent.RemoveResource(ResourceType.Blood);
            }
            
            if (!hasFlesh)
            {
                corpseComponent.RemoveResource(ResourceType.Flesh);
            }
            
            if (!hasBones)
            {
                corpseComponent.RemoveResource(ResourceType.Bones);
            }

            return corpseComponent;
        }
        
        public async Task<Corpse> CreateDefaultCorpse(Vector3 spawnPosition)
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<Corpse>(CORPSE_ADDRESS);
            var gameObject = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            var corpseComponent = gameObject.GetComponent<Corpse>();

            return corpseComponent;
        }
    }
}