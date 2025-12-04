using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class SummonsInstaller : MonoInstaller<SummonsInstaller>
    {
        [SerializeField] private SkeletonMage _skeletonMagePrefab;
        
        public override void InstallBindings()
        {
            Container
                .Bind<SkeletonMage>()
                .FromComponentInNewPrefab(_skeletonMagePrefab)
                .AsTransient();
        }
    }
}