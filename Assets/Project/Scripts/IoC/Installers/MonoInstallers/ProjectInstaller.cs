using Itibsoft.PanelManager.External;
using Project.Scripts.App;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        public override void InstallBindings()
        {
            PanelManagerInstaller.Install(Container, default, null);
            
            Container
                .Bind<IAssetLoader>()
                .To<AssetLoader>()
                .AsSingle();

            Container
                .Bind<ProjectEntryPoint>()
                .AsSingle();
        }
    }
}