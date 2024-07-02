using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private PlayerMover _playerMover;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IPlayer>()
                .To<PlayerMover>()
                .FromInstance(_playerMover)
                .AsSingle();
        }
    }
}