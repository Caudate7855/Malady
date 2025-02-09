using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ChurchInstaller : MonoInstaller<ChurchInstaller>
    {
        public override void InstallBindings()
        {
            InstallLocationFactories();
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
    }
}