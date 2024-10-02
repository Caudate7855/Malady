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
        }
    }
}