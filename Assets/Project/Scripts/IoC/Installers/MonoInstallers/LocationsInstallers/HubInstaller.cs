using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class HubInstaller : MonoInstaller<HubInstaller>
    {
        [SerializeField] private BookInteractable _bookInteractable;
        [SerializeField] private Blacksmith _blacksmith;
        [SerializeField] private Exit _exit;
        
        public override void InstallBindings()
        {
            InstallLocationFactories();
            InstallCoreFactories();
            InstallInteractableFactories();
            InstallNpcFactories();
        }

        private void InstallLocationFactories()
        {
            Container
                .Bind<HubFactory>()
                .AsSingle();
        }

        private void InstallNpcFactories()
        {
            Container
                .Bind<Blacksmith>()
                .FromInstance(_blacksmith)
                .AsSingle();
        }
        
        private void InstallInteractableFactories()
        {
            Container
                .Bind<InteractableFactory>()
                .AsSingle();

            Container
                .Bind<BookInteractable>()
                .FromInstance(_bookInteractable)
                .AsSingle();

            Container
                .Bind<Exit>()
                .FromInstance(_exit)
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