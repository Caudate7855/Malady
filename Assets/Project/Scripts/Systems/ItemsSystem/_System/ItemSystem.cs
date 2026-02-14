using System;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Project.Scripts
{
    public class ItemSystem : IInitializable, IDisposable
    {
        private readonly ItemsConfig _itemsConfig;
        private readonly ItemsFactory _itemsFactory;
        private readonly WorldDropsController _worldDropsController;

        public ItemSystem(ItemsConfig itemsConfig, ItemsFactory itemsFactory, WorldDropsController worldDropsController)
        {
            _itemsConfig = itemsConfig;
            _itemsFactory = itemsFactory;
            _worldDropsController = worldDropsController;
        }

        public ItemData CreateRandomItem()
        {
            return _itemsFactory.CreateRandomItem();
        }

        public ItemData CreateItemByType(ItemType itemType)
        {
            return _itemsFactory.CreateItemByType(itemType);
        }

        public void DropItem(ItemData itemData, Vector3 position)
        {
            var config = _itemsConfig.GetItemConfigByType(itemData.ItemType);

            var worldGo = new GameObject($"Drop: {itemData.ItemType}");
            worldGo.transform.position = position;

            var col = worldGo.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.35f;

            worldGo.AddComponent<Outline>();

            var worldDrop = worldGo.AddComponent<WorldDropItem>();
            worldDrop.Setup(itemData, config.DropSprite);

            _worldDropsController.Register(worldDrop);
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}