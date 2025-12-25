using Cysharp.Threading.Tasks;
using DunGen;
using UnityEngine;

namespace Project.Scripts
{
    public sealed class DungeonGenerationBoot : LevelBootBase
    {
        [SerializeField] private RuntimeDungeon _runtimeDungeon;

        public override void Initialize()
        {
            base.Initialize();

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
    }
}