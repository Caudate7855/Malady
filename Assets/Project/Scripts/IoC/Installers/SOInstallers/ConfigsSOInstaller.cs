using Project.Scripts.Configs;
using Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    [CreateAssetMenu(fileName = nameof(ConfigsSOInstaller), menuName = "Installers/" + nameof(ConfigsSOInstaller))]
    public class ConfigsSOInstaller : ScriptableObjectInstaller<ConfigsSOInstaller>
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private SpellsConfig _spellsConfig;
        [SerializeField] private ItemsConfig _itemsConfig;
        [SerializeField] private StatsConfig _statsConfig;
        [SerializeField] private ResourcesConfig _resourcesConfig;
        
        public override void InstallBindings()
        {
            Container
                .Bind<PlayerConfig>()
                .FromInstance(_playerConfig)
                .AsSingle();
            
            Container
                .Bind<ItemsConfig>()
                .FromInstance(_itemsConfig)
                .AsSingle();

            Container
                .Bind<SpellsConfig>()
                .FromInstance(_spellsConfig)
                .AsSingle();
            
            Container
                .Bind<ResourcesConfig>()
                .FromInstance(_resourcesConfig)
                .AsSingle();
            
            Container
                .Bind<StatsConfig>()
                .FromInstance(_statsConfig)
                .AsSingle();
        }
    }
}