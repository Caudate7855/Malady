using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts
{
    public class DungeonFactory : MonoBehaviour
    {
        public async Task<Dungeon> Create(string path)
        {
            AssetLoader assetLoader = new();
            var instance = await assetLoader.Load<Dungeon>(path);
            return instance;
        }
    }
}