using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class SpellsInstaller : MonoInstaller<SpellsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<SpellsLogicsList>()
                .AsSingle();
            
            Container
                .Bind<SpellBase>()
                .To<SummonSkeletonMageSpell>()
                .AsSingle();
            
        }
    }
}