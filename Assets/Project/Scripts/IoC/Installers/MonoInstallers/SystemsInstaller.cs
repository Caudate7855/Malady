using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class SystemsInstaller : MonoInstaller<SystemsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<DialogueSystemManager>()
                .AsSingle();
                        
            Container
                .Bind<SpellSystemController>()
                .AsSingle();

            Container
                .Bind<SummonSystem>()
                .AsSingle();

            Container
                .Bind<PlayerSpellModificatorsSystem>()
                .AsSingle();
        }
    }
}