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
                .BindInterfacesAndSelfTo<SpellSystem>()
                .AsSingle();

            Container
                .Bind<SummonSystem>()
                .AsSingle();
            
            Container
                .Bind<StatSystem>()
                .AsSingle();
        }
    }
}