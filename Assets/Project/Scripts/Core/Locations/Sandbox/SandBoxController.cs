using System;
using Project.Scripts.Interfaces;
using Unity.AI.Navigation;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class SandBoxController : MonoBehaviour, ICustomInitializable
    {
        private NavMeshSurface _navMeshSurface;

        public PlayerController PlayerControllerObject { get; set; }

        public void Initialize()
        {
            _navMeshSurface = GetComponent<NavMeshSurface>();
            _navMeshSurface.BuildNavMesh();
        }

        public GameObject GetBookParentObject()
        {
            return GetComponentInChildren<BookPositionMarker>().gameObject;
        }

        public GameObject GetExitParentObject()
        {
            return GetComponentInChildren<ExitPositionMarker>().gameObject;
        }
        
        public Vector3 GetNpcSpawnPosition(NpcTypes npcType)
        {
            var spawnPoints = GetComponentsInChildren<NpcSpawnPoint>();

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (spawnPoints[i].GetSpawnPointType() == npcType)
                {
                    return spawnPoints[i].transform.position;
                }
            }

            throw new Exception($"Cannot find spawn point for {npcType}");
        }
    }
}