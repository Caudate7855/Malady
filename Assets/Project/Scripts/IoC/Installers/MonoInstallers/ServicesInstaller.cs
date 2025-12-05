using Project.Scripts.CorpseSystem;
using Project.Scripts.Services;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ServicesInstaller : MonoInstaller<ServicesInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IAssetLoader>()
                .To<AssetLoader>()
                .AsSingle();
            
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .FromNew()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<MouseController>()
                .AsSingle();
            
            Container
                .Bind<ResourceFinder>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<MouseFsm>()
                .AsSingle();
        }
    }
}