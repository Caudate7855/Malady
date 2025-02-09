using System;
using Project.Scripts.Interfaces;
using Unity.AI.Navigation;
using UnityEngine;
using Zenject;

namespace Project.Scripts.Core.Hub
{
    public class HubController : MonoBehaviour, ICustomInitializable
    {
        private NavMeshSurface _navMeshSurface;
        
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