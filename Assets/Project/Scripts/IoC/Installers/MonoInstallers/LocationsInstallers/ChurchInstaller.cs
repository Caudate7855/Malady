using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ChurchInstaller : MonoInstaller<ChurchInstaller>
    {
        public override void InstallBindings()
        {
            InstallLocationFactories();
            InstallCoreFactories();
            InstallInteractableFactories();
        }

        private void InstallLocationFactories()
        {
            Container
                .Bind<ChurchFactory>()
                .AsSingle();
        }
        
        private void InstallInteractableFactories()
        {
            Container
                .Bind<InteractableFactory>()
                .AsSingle();
        }
        
        private void InstallCoreFactories()
        {
            Container
                .Bind<PlayerFactory>()
                .AsSingle();

            Container
                .Bind<EnemyFactory>()
                .AsSingle();
        }
    }
}