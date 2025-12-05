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
                .BindInterfacesAndSelfTo<PlayerController>()
                .FromComponentInNewPrefab(_playerController)
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<PlayerMover>()
                .AsSingle();

            Container
                .Bind<PlayerStats>()
                .AsSingle();

            Container
                .Bind<PlayerFsm>()
                .AsSingle();
            
            Container
                .Bind<EnemyStats>()
                .AsTransient();
            
            Container
                .BindInterfacesAndSelfTo<InputController>()
                .AsSingle();
        }
    }
}