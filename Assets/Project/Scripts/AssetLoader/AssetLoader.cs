using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.Scripts
{
    public class AssetLoader
    {
        private GameObject _cachedObject;

        public async Task<T> Load<T>(string assetId)
        {
            var handle = Addressables.InstantiateAsync(assetId);

            _cachedObject = await handle.Task;

            if (_cachedObject.TryGetComponent(out T component) == false)
            {
                throw new NullReferenceException($"Object of type {typeof(T)} is null");
            }

            return component;
        }
    }
}