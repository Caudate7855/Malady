using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class SystemsInstaller : MonoInstaller<SystemsInstaller>
    {
        [SerializeField] private InventoryItem _inventoryItem;
        [SerializeField] private SpellItem _spellItem;
        [SerializeField] private DropItemUIView _dropItemUIView;
        
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

            BindItemSystem();
            BindDragAndDropSystem();
        }

        private void BindItemSystem()
        {
            Container
                .BindInterfacesAndSelfTo<ItemSystem>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<DropSystem>()
                .AsSingle();
            
            Container
                .BindInterfacesAndSelfTo<WorldDropsController>()
                .AsSingle();
            
            Container
                .Bind<DropItemUIView>()
                .FromInstance(_dropItemUIView)
                .AsSingle();
            
            Container
                .Bind<ItemsFactory>()
                .AsSingle();
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