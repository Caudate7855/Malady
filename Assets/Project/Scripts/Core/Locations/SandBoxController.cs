using System;
using Unity.AI.Navigation;
using UnityEngine;

namespace Project.Scripts
{
    public class SandBoxController : MonoBehaviour
    {
        [SerializeField] private NavMeshSurface _navMeshSurface;

        public void Initialize()
        {
            _navMeshSurface.BuildNavMesh();
        }

        public GameObject GetBookPosition()
        {
            return FindFirstObjectByType<BookPositionMarker>().gameObject;
        }

        public GameObject GetExitPosition()
        {
            return FindFirstObjectByType<BookPositionMarker>().gameObject;
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