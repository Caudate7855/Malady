using Cysharp.Threading.Tasks;
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
    public class GlobalFactory
    {
        private DiContainer _diContainer;
        private readonly IAssetLoader _assetLoader;

        [Inject] private BookInteractable _bookInteractable;
        [Inject] private ExitInteractable _exitInteractable;

        public GlobalFactory(IAssetLoader assetLoader, DiContainer diContainer)
        {
            _diContainer = diContainer;
            _assetLoader = assetLoader;
        }

        public async UniTask<T> CreateAsync<T>(string assetAddress, Vector3 position = default)
            where T : Object
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<Object>(assetAddress);
            var gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
            var component = gameObject.GetComponent<T>();

            return component;
        }

        public async UniTask<T> CreateAndInitializeAsync<T>(string assetAddress, Vector3 position = default)
            where T : Object, ICustomInitializable
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<Object>(assetAddress);
            var gameObject = Object.Instantiate(prefab, position, Quaternion.identity);
            var component = gameObject.GetComponent<T>();
            component.Initialize();

            return component;
        }

        public async UniTask<T> CreateAndInjectAsync<T>(string assetAddress, Vector3 spawnPosition = default)
            where T : Object
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<T>(assetAddress);
            var gameObject = Object.Instantiate(prefab, spawnPosition, Quaternion.identity);
            var component = gameObject.GetComponent<T>();
            _diContainer.Inject(component);

            return component;
        }
        
        public async UniTask<T> CreateForComponentAndInitialize<T>(string assetAddress, Vector3 spawnPosition) where T : Object
        {
            var prefab = await _assetLoader.LoadGameObjectAsync<PlayerController>(assetAddress);
            
            var gameObject = _diContainer.InstantiatePrefabForComponent<PlayerController>(prefab);
            var component = gameObject.GetComponent<PlayerController>();
            
            component.Initialize();
            
            return component as T;
        }
        
        #region Characters

        public async UniTask<PlayerController> CreatePlayer(Vector3 spawnPosition)
        {
            return await CreateForComponentAndInitialize<PlayerController>("Player",spawnPosition);
        }

        public async UniTask<T> CreateNpcAsync<T>(string npcAssetAddress, Vector3 spawnPosition) where T : NpcBase
        {
            return await CreateAndInjectAsync<T>(npcAssetAddress, spawnPosition);
        }
        
        public async UniTask<T> CreateEnemyAsync<T>(string enemyAssetAddress, Vector3 spawnPosition) where T : EnemyBase
        {
            return await CreateAndInitializeAsync<T>(enemyAssetAddress, spawnPosition);
        }

        public async UniTask<T> CreateSummonAsync<T>(string summonUnitAssetAddress, Vector3 spawnPosition) where T : SummonUnitBase
        {
            return await CreateAndInitializeAsync<T>(summonUnitAssetAddress, spawnPosition);
        }

        #endregion

        #region Corpse

        public async UniTask<Corpse> CreateDefaultCorpseAsync(Vector3 spawnPosition)
        {
            return await CreateAsync<Corpse>("Corpse", spawnPosition);
        }

        public async UniTask<Corpse> CreateCustomCorpseAsync(bool hasSoul, bool hasBlood, bool hasFlesh, bool hasBones,
            Vector3 spawnPosition)
        {
            var corpseComponent = await CreateDefaultCorpseAsync(spawnPosition);

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

        #endregion
        
        #region Interactable

        public void CreateBook(GameObject parentObject)
        {
            if (parentObject != null)
            {
                _diContainer.InstantiatePrefabForComponent<BookInteractable>(_bookInteractable, parentObject.transform);
            }
        }

        public void CreateExit(GameObject parentObject)
        {
            if (parentObject != null)
            {
                _diContainer.InstantiatePrefabForComponent<ExitInteractable>(_exitInteractable, parentObject.transform);
            }
        }

        #endregion
    }
}