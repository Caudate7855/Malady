using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class HubInstaller : MonoInstaller<HubInstaller>
    {
        [SerializeField] private Blacksmith _blacksmith;
        
        public override void InstallBindings()
        {
            InstallNpcFactories();
        }

        private void InstallNpcFactories()
        {
            Container
                .Bind<Blacksmith>()
                .FromInstance(_blacksmith)
                .AsSingle();
        }
    }
}