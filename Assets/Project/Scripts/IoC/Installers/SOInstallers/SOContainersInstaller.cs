using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    [CreateAssetMenu(fileName = "SOContainersInstaller", menuName = "Installers/SOContainersInstaller")]
    public class SOContainersInstaller : ScriptableObjectInstaller<SOContainersInstaller>
    {
        [SerializeField] private SpellsContainerSo _spellsContainerSo;
        [SerializeField] private ItemsContainerSo _itemsContainerSo;
        [SerializeField] private SpellModificatorsConfigsContainer _spellModificatorsConfigsContainer;
        
        public override void InstallBindings()
        {
            Container
                .Bind<ItemsContainerSo>()
                .FromInstance(_itemsContainerSo)
                .AsSingle();
            
            Container
                .Bind<SpellsContainerSo>()
                .FromInstance(_spellsContainerSo)
                .AsSingle();

            Container
                .Bind<SpellModificatorsConfigsContainer>()
                .FromInstance(_spellModificatorsConfigsContainer)
                .AsSingle();
        }
    }
}