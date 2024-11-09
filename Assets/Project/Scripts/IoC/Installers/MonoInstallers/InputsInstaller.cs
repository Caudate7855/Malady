using Project.Scripts.Core;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class InputsInstaller : MonoInstaller<InputsInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerInputController>()
                .AsSingle();
        }
    }
}