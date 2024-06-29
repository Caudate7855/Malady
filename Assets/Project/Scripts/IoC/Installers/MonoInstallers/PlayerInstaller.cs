using Project.Scripts.Core.Interfaces;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class PlayerInstaller : MonoInstaller<PlayerInstaller>
    {
        [SerializeField] private IPlayer _player;
        
        public override void InstallBindings()
        {
            Container
                .Bind<IPlayer>()
                .FromInstance(_player)
                .AsSingle();
        }
    }
}