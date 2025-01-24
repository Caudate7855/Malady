using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Project.Scripts.Services
{
    [UsedImplicitly]
    public class AssetLoader : IAssetLoader
    {
        public async UniTask<T> LoadGameObjectAsync<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(path);

            var loadedObject = await handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var component = loadedObject.GetComponent<T>();

                if (component != null)
                {
                    return component;
                }
            }

            throw new Exception($"Cannot load GameObject with path: {path}");
        }

        public async UniTask<T> LoadNotGameObjectAsync<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<T>(path);
            var result = await handle;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return result;
            }

            throw new Exception($"Failed to load TextAsset from path: {path}");
        }
    }
}