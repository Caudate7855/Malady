using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class EnemiesInstaller : MonoInstaller<EnemiesInstaller>
    {
        [SerializeField] private EnemyRange _enemyRange;
        
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<EnemyRange>()
                .FromInstance(_enemyRange)
                .AsTransient();
        }
    }
}