using Itibsoft.PanelManager.External;
using Project.Scripts.App;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class ProjectInstaller : MonoInstaller<ProjectInstaller>
    {
        [SerializeField] private GameDirector _gameDirector;
        
        public override void InstallBindings()
        {
            PanelManagerInstaller.Install(Container, default, null);

            Container
                .Bind<IAssetLoader>()
                .To<AssetLoader>()
                .AsSingle();

            Container
                .Bind<ProjectEntryPoint>()
                .AsSingle();

            Container
                .Bind<GameDirector>()
                .FromInstance(_gameDirector)
                .AsSingle();

            Container
                .Bind<IStatSystem>()
                .To<StatsSystem>()
                .AsSingle();
        }
    }
}