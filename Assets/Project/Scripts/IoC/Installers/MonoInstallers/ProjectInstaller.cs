using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IAssetLoader>()
                .To<AssetLoader>()
                .AsSingle();
        }
    }
}