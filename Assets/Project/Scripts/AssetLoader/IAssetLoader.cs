using System.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts
{
    public interface IAssetLoader
    {
        public GameObject CashedObject { get; set; }
        public Task<T> Load<T>(string path);
        public void Unload();
    }
}