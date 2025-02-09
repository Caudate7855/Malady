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
        private GameDirector _gameDirector;

        public override void InstallBindings()
        {
            PanelManagerInstaller.Install(Container, default, null);

            _gameDirector = FindObjectOfType<GameDirector>();

            Container
                .Bind<HubBoot>()
                .AsSingle();

            Container
                .Bind<ChurchBoot>()
                .AsSingle();

            Container
                .Bind<GameDirector>()
                .FromInstance(_gameDirector)
                .AsSingle();
        }
    }
}