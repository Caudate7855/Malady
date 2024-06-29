using System.Threading.Tasks;
using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts
{
    public class PlayerFactory : MonoBehaviour
    {
        public async Task<Player> Create(string path)
        {
            AssetLoader assetLoader = new();
            var instance = await assetLoader.Load<Player>(path);
            return instance;
        }
    }
}