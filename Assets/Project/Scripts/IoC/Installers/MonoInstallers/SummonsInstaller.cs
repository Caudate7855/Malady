using Project.Scripts.Summons;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class SummonsInstaller : MonoInstaller<SummonsInstaller>
    {
        [SerializeField] private SkeletonMage _skeletonMagePrefab;
        [SerializeField] private SkeletonArcher _skeletonArcherPrefab;
        
        public override void InstallBindings()
        {
            Container
                .Bind<SkeletonMage>()
                .FromComponentInNewPrefab(_skeletonMagePrefab)
                .AsTransient();
            
            Container
                .Bind<SkeletonArcher>()
                .FromComponentInNewPrefab(_skeletonArcherPrefab)
                .AsTransient();
        }
    }
}