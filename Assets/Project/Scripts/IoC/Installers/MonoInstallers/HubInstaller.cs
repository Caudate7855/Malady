using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class HubInstaller : MonoInstaller<HubInstaller>
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