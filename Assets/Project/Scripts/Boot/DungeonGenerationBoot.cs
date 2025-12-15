using DunGen;
using Unity.AI.Navigation;
using UnityEngine;

namespace Project.Scripts
{
    public sealed class DungeonGenerationBoot : LevelBootBase
    {
        [SerializeField] private RuntimeDungeon _runtimeDungeon;

        private NavMeshSurface _navMeshSurface;

        public override async void Initialize()
        {
            _navMeshSurface = _runtimeDungeon.GetComponent<NavMeshSurface>();

            if (_navMeshSurface == null)
            {
                _navMeshSurface = _runtimeDungeon.gameObject.AddComponent<NavMeshSurface>();
                _navMeshSurface.collectObjects = CollectObjects.All;
            }

            _runtimeDungeon.Generator.OnGenerationComplete += OnDungeonGenerated;
            _runtimeDungeon.Generate();
        }

        private void OnDungeonGenerated(DungeonGenerator generator)
        {
            _runtimeDungeon.Generator.OnGenerationComplete -= OnDungeonGenerated;

            _navMeshSurface.BuildNavMesh();
        }
    }
}