using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class HubInstaller : MonoInstaller<HubInstaller>
    {
        [SerializeField] private BookInteractable _bookInteractable;
        
        public override void InstallBindings()
        {
            Container
                .Bind<DungeonFactory>()
                .AsSingle();

            Container
                .Bind<PlayerFactory>()
                .AsSingle();

            Container
                .Bind<EnemyFactory>()
                .AsSingle();

            Container
                .Bind<BookInteractable>()
                .FromInstance(_bookInteractable)
                .AsSingle();
        }
    }
}