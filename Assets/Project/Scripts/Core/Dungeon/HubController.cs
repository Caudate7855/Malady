using Project.Scripts.Interfaces;
using Unity.AI.Navigation;
using UnityEngine;

namespace Project.Scripts.Core.Dungeon
{
    public class HubController : MonoBehaviour, ICustomInitializable
    {
        private NavMeshSurface _navMeshSurface;
        
        public void Initialize()
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
            _navMeshSurface.BuildNavMesh();
        }
    }
}