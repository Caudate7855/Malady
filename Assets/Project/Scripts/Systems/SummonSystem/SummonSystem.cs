using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SummonSystem
    {
        [Inject] private GlobalFactory _globalFactory;

        private const string SkeletonWarriorAddress = "SummonSkeletonWarrior";
        private const string SkeletonArcherAddress = "SummonSkeletonArcher";
        private const string SkeletonMageAddress = "SummonSkeletonMage";

        public async UniTask<SkeletonWarrior> CreateSkeletonWarriorAsync(Vector3 spawnPosition)
        {
            return await _globalFactory.CreateSummonAsync<SummonUnitBase>(SkeletonWarriorAddress, spawnPosition) as
                SkeletonWarrior;
        }

        public async UniTask<SkeletonArcher> CreateSkeletonArcherAsync(Vector3 spawnPosition)
        {
            return await _globalFactory.CreateSummonAsync<SummonUnitBase>(SkeletonArcherAddress, spawnPosition) as
                SkeletonArcher;
        }

        public async UniTask<SkeletonMage> CreateSkeletonMageAsync(Vector3 spawnPosition)
        {
            return await _globalFactory.CreateSummonAsync<SummonUnitBase>(SkeletonMageAddress, spawnPosition) as
                SkeletonMage;
        }
    }
}