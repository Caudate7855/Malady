using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
        [SerializeField] private DungeonFactory _dungeonFactory;
        [SerializeField] private PlayerFactory _playerFactory;

        public override void InstallBindings()
        {
            Container
                .Bind<DungeonFactory>()
                .FromInstance(_dungeonFactory)
                .AsSingle();

            Container
                .Bind<PlayerFactory>()
                .AsSingle();
        }
    }
}