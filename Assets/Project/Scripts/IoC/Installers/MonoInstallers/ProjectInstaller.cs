using Itibsoft.PanelManager.External;
using Project.Scripts.App;
using Project.Scripts.Core;
using Project.Scripts.Services;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        private Main _main;

        public override void InstallBindings()
        {
            PanelManagerInstaller.Install(Container, default, null);

            _main = FindObjectOfType<Main>();

            Container
                .Bind<SandBoxBoot>()
                .AsSingle();
            
            Container
                .Bind<HubBoot>()
                .AsSingle();

            Container
                .Bind<ChurchBoot>()
                .AsSingle();

            Container
                .Bind<Main>()
                .FromInstance(_main)
                .AsSingle();
        }
    }
}