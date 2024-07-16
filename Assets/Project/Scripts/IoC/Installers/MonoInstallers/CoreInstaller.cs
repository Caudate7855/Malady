using Project.Scripts.Core;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class CoreInstaller : MonoInstaller<CoreInstaller>
    {
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
        }
    }
}