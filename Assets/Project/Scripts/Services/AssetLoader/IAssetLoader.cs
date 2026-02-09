using Cysharp.Threading.Tasks;

namespace Project.Scripts
{
    public interface IAssetLoader
    {
        public UniTask<T> LoadGameObjectAsync<T>(string path);
        public UniTask<T> LoadNotGameObjectAsync<T>(string path);
    }
}