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
            
            Container
                .Bind<PlayerFactory>()
                .AsSingle();

            Container
                .Bind<EnemyFactory>()
                .AsSingle();
            
            Container
                .Bind<CorpseFactory>()
                .AsSingle();
        }
    }
}