using Cysharp.Threading.Tasks;
using DunGen;
using DunGen.DungeonCrawler;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts
{
    public sealed class DungeonGenerationBoot : LevelBootBase
    {
        [SerializeField] private RuntimeDungeon _runtimeDungeon;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        private NavMeshAgent _agent;

        public override void Initialize()
        {
            _agent = PlayerController.GetComponent<NavMeshAgent>();
            _agent.enabled = false;

            _runtimeDungeon.Generator.OnGenerationStatusChanged += OnDungeonGenerationStatusChanged;
            _runtimeDungeon.Generate();
        }

        private void OnDungeonGenerationStatusChanged(
            DungeonGenerator generator,
            GenerationStatus status)
        {
            if (status != GenerationStatus.Complete)
            {
                return;
            }

            _runtimeDungeon.Generator.OnGenerationStatusChanged -= OnDungeonGenerationStatusChanged;
            BuildNavMeshAndSpawnAsync().Forget(Debug.LogException);
        }

        private async UniTask BuildNavMeshAndSpawnAsync()
        {
            await UniTask.Yield();

            _navMeshSurface.BuildNavMesh();

            await UniTask.Yield();

            var spawn = FindFirstObjectByType<PlayerSpawn>();
            
            if (spawn == null)
            {
                Debug.LogError("PlayerSpawn not found in scene");
                return;
            }

            _agent.Warp(spawn.transform.position);
            _agent.enabled = true;
        }
    }
}