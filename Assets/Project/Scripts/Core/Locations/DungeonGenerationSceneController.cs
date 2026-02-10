using Project.Scripts.Player;
using Unity.AI.Navigation;
using UnityEngine;

namespace Project.Scripts
{
    public class DungeonGenerationSceneController : MonoBehaviour
    {
        private NavMeshSurface _navMeshSurface;
        private PlayerController _playerController;

        public void Initialize()
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
            _navMeshSurface.BuildNavMesh();
        }

        public void Initialize(PlayerController playerController)
        {
            
        }
    }
}