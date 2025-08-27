using Project.Scripts.UI.Inventory;
using UnityEngine;
using Zenject;

namespace Project.Scripts.IoC.Installers
{
    public class InventoryInstaller : MonoInstaller<InventoryInstaller>
    {
        [SerializeField] private InventoryItem _item;
            
        public override void InstallBindings()
        {
            Container
                .Bind<InventoryItem>()
                .FromInstance(_item)
                .AsSingle();
        }
    }
}