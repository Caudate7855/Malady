using Project.Scripts.Core;
using Project.Scripts.Services;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ServicesInstaller : MonoInstaller<ServicesInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .FromNew()
                .AsSingle();
            
            Container
                .Bind<MouseController>()
                .AsSingle();

            Container
                .BindInterfacesAndSelfTo<PlayerMover>()
                .AsSingle();
        }
    }
}