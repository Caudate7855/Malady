using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Project.Scripts
{
    public class AssetLoader : IAssetLoader
    {
        public GameObject CashedObject { get; set; }

        public async Task<T> Load<T>(string path)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>(path);
            CashedObject = await handle.Task;
            
            var result = handle.Result.GetComponent<T>();
            
            return result;
        }

        public void Unload()
        {
            if (CashedObject != null)
            {
                Addressables.ReleaseInstance(CashedObject);
                CashedObject = null;
            }
        }
    }
}