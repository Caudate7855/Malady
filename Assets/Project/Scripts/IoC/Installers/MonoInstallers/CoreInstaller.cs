using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerFactory>()
                .AsSingle();

            Container
                .Bind<DungeonFactory>()
                .AsSingle();
            
            Container
                .Bind<IAssetLoader>()
                .To<AssetLoader>()
                .AsSingle();
        }
    }
}