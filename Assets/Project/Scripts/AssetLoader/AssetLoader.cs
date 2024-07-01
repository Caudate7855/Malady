using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.Scripts
{
    public static class AssetLoader
    {
        public static async Task<T> Load<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<Object>(path);
            await handle.Task;
            
            var result = handle.Result.GetComponent<T>();
            
            Addressables.Release(handle);
            
            return result;
        }
    }
}