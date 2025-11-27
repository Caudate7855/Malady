using System;
using Project.Scripts.Interfaces;
using Unity.AI.Navigation;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class ChurchController : MonoBehaviour, ICustomInitializable
    {
        private NavMeshSurface _navMeshSurface;
        private PlayerController _playerController;

        public PlayerController PlayerControllerObject { get; set; }

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