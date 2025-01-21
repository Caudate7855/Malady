using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class FactoriesInstaller : MonoInstaller<FactoriesInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<NpcFactory>()
                .AsSingle();
        }
    }
}