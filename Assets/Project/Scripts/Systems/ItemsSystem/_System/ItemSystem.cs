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
        private readonly DropSystem _dropSystem;

        public ItemSystem(ItemsConfig itemsConfig, ItemsFactory itemsFactory, DropSystem dropSystem)
        {
            _itemsConfig = itemsConfig;
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

        public void GetItem(ItemData itemData)
        {
            Debug.Log($"Item collected:{itemData.ItemType}");
        }

        public void DropItem(ItemData itemData, Vector3 position)
        {
            var config = _itemsConfig.GetItemConfigByType(itemData.ItemType);

            var worldItem = new GameObject($"Drop: {itemData.ItemType}");
            worldItem.transform.position = position;

            _dropSystem.SpawnFollow(config.DropSprite, worldItem.transform, () =>
            {
                GetItem(itemData);
                UnityEngine.Object.Destroy(worldItem);
            });
        }

        public void Initialize()
        {
            
        }

        public void Dispose()
        {
        }
    }
}