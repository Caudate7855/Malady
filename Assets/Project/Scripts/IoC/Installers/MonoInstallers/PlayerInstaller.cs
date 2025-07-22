using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private PlayerController _playerController;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerController>()
                .FromInstance(_playerController)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<PlayerMover>()
                .AsSingle();

            Container
                .Bind<PlayerStats>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<PlayerFsm>()
                .AsSingle();

            Container
                .Bind<IStatSystem>()
                .To<StatsSystem>()
                .AsSingle();
            
            Container
                .Bind<PlayerInputController>()
                .AsSingle();
        }
    }
}