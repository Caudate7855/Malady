using Project.Scripts.Core;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private Player _player;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IPlayer>()
                .To<Player>()
                .FromInstance(_player)
                .AsSingle();
        }
    }
}