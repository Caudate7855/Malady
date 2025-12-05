using Itibsoft.PanelManager.External;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        private Main _main;

        public override void InstallBindings()
        {
            PanelManagerInstaller.Install(Container, default, null);

            _main = FindFirstObjectByType<Main>();

            Container
                .Bind<Main>()
                .FromInstance(_main)
                .AsSingle();
        }
    }
}