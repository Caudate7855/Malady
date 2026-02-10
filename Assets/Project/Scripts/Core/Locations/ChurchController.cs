using Project.Scripts.Player;
using Unity.AI.Navigation;
using UnityEngine;

namespace Project.Scripts
{
    public class ChurchController : MonoBehaviour
    {
        private NavMeshSurface _navMeshSurface;
        private PlayerController _playerController;

        public PlayerController PlayerControllerObject { get; set; }

        public void Initialize()
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
            _navMeshSurface.BuildNavMesh();
        }
    }
}