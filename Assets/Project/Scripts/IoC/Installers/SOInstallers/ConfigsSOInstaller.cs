using Project.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    [CreateAssetMenu(fileName = nameof(ConfigsSOInstaller), menuName = "Installers/" + nameof(ConfigsSOInstaller))]
    public class ConfigsSOInstaller : ScriptableObjectInstaller<ConfigsSOInstaller>
    {
        [SerializeField] private SpellsConfig _spellsConfig;
        [SerializeField] private ItemsConfig _itemsConfig;
        [SerializeField] private StatsConfig _statsConfig;
        [SerializeField] private ResourceConfig _resourceConfig;
        
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
                .Bind<ResourceConfig>()
                .FromInstance(_resourceConfig)
                .AsSingle();
            
            Container
                .Bind<StatsConfig>()
                .FromInstance(_statsConfig)
                .AsSingle();
        }
    }
}