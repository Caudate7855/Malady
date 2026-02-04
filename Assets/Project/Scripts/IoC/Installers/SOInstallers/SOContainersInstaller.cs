using Project.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    [CreateAssetMenu(fileName = "SOContainersInstaller", menuName = "Installers/SOContainersInstaller")]
    public class SOContainersInstaller : ScriptableObjectInstaller<SOContainersInstaller>
    {
        [SerializeField] private SpellsConfig _spellsConfig;
        [SerializeField] private ItemsConfig _itemsConfig;
        [SerializeField] private StatsConfig _statsConfig;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ItemsConfig>()
                .FromInstance(_itemsConfig)
                .AsSingle();

            Container
                .Bind<SpellsConfig>()
                .FromInstance(_spellsConfig)
                .AsSingle();
            
            Container
                .Bind<StatsConfig>()
                .FromInstance(_statsConfig)
                .AsSingle();
        }
    }
}