using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Services
{
    public interface IAssetLoader
    {
        public UniTask<T> LoadGameObjectAsync<T>(string path);
        public UniTask<T> LoadNotGameObjectAsync<T>(string path);
    }
}