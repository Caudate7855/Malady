using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class SystemsInstaller : MonoInstaller<SystemsInstaller>
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField] private SpellItem _spellItem;
        
        public override void InstallBindings()
        {
            Container
                .Bind<DialogueSystemManager>()
                .AsSingle();
                        
            Container
                .BindInterfacesAndSelfTo<SpellSystem>()
                .AsSingle();

            Container
                .Bind<StatSystem>()
                .AsSingle();

            BindDragAndDropSystem();
        }

        private void BindDragAndDropSystem()
        {
            Container
                .BindInterfacesAndSelfTo<DragAndDropSystem>()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<InventoryItem>()
                .FromInstance(_inventoryItem)
                .AsSingle();
            
            Container
                .Bind<SpellItem>()
                .FromInstance(_spellItem)
                .AsSingle();
        }
    }
}