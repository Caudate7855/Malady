using System;
using UnityEngine;
using Zenject;
using Vector3 = UnityEngine.Vector3;

namespace Project.Scripts
{
    public class ItemSystem : IInitializable, IDisposable
    {
        private readonly ItemsConfig _itemsConfig;
        private readonly DropIconsConfig _dropIconsConfig;
        private readonly ItemsFactory _itemsFactory;
        private readonly DropSystem _dropSystem;

        public ItemSystem(ItemsConfig itemsConfig, DropIconsConfig dropIconsConfig, ItemsFactory itemsFactory, DropSystem dropSystem)
        {
            _itemsConfig = itemsConfig;
            _dropIconsConfig = dropIconsConfig;
            _itemsFactory = itemsFactory;
            _dropSystem = dropSystem;
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
            var config = _itemsConfig.GetItemConfigByType(itemData.Type);

            var worldGo = new GameObject($"Drop: {itemData.Type}");
            worldGo.transform.position = position;

            var col = worldGo.AddComponent<SphereCollider>();
            col.isTrigger = true;
            col.radius = 0.35f;

            worldGo.AddComponent<Outline>();

            var sprite = _dropIconsConfig.GetItemDropIcon(itemData.Type);
            
            var worldDrop = worldGo.AddComponent<WorldDropItem>();
            worldDrop.Setup(itemData, sprite);

            _dropSystem.Register(worldDrop);
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}