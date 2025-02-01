using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class HubInstaller : MonoInstaller<HubInstaller>
    {
        [SerializeField] private BookInteractable _bookInteractable;
        [SerializeField] private Blacksmith _blacksmith;
        
        public override void InstallBindings()
        {
            Container
                .Bind<HubFactory>()
                .AsSingle();

            Container
                .Bind<PlayerFactory>()
                .AsSingle();

            Container
                .Bind<EnemyFactory>()
                .AsSingle();
            
            Container
                .Bind<InteractableFactory>()
                .AsSingle();

            Container
                .Bind<BookInteractable>()
                .FromInstance(_bookInteractable)
                .AsSingle();
            
            Container
                .Bind<Blacksmith>()
                .FromInstance(_blacksmith)
                .AsSingle();
        }
    }
}