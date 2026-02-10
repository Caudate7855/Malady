using Project.Scripts.Player;
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
                .Bind<PlayerFsm>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<InputController>()
                .AsSingle();
        }
    }
}