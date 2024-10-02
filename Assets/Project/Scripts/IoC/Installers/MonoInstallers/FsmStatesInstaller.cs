using Project.Scripts.FSM;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class FsmStatesInstaller : MonoInstaller<FsmStatesInstaller>
    {
        public override void InstallBindings()
        {
            Container
                .Bind<GameStateBase>()
                .AsSingle();
        }
    }
}