using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class DialogueInstaller : MonoInstaller<DialogueInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<DialogueSystemManager>()
                .AsSingle();
        }
    }
}