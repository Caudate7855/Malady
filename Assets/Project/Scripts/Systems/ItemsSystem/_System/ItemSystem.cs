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

        public Item CreateRandomItem()
        {
            return _itemsFactory.CreateRandomItem();
        }
        
        public Item CreateItemByType(ItemType itemType)
        {
            return _itemsFactory.CreateItemByType(itemType);
        }

        public void GetItem(Item item)
        {
            Debug.Log($"Item collected:{item.ItemType}");
        }

        public void DropItem(Item item, Vector3 position)
        {
            var config = _itemsConfig.GetItemConfigByType(item.ItemType);

            var worldItem = new GameObject($"Drop: {item.ItemType}");
            worldItem.transform.position = position;

            _dropSystem.SpawnFollow(config.DropSprite, worldItem.transform, () =>
            {
                GetItem(item);
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