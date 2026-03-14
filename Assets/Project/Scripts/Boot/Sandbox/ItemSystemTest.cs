using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class ItemSystemTest : MonoBehaviour
    {
        [SerializeField] private bool _isItemLogging;
        [SerializeField] private ItemType _itemType;

        [Inject] private ItemSystem _itemSystem;

        [Button]
        public void CreateRandomItem()
        {
            var item = _itemSystem.CreateRandomItem();

            if (_isItemLogging)
            {
                LogItem(item);
            }
        }

        [Button]
        public void CreateItemByType()
        {
            var item = _itemSystem.CreateItemByType(_itemType);

            if (_isItemLogging)
            {
                LogItem(item);
            }
        }
    
        [Button]
        public void DropRandomItem()
        {
            var item = _itemSystem.CreateRandomItem();
    
            _itemSystem.DropItem(item, Vector3.zero);

            if (_isItemLogging)
            {
                LogItem(item);
            }
        }

        [Button]
        public void DropItemByType()
        {
            var item = _itemSystem.CreateItemByType(_itemType);
            _itemSystem.DropItem(item, Vector3.zero);
        }

        private void LogItem(ItemData item)
        {
            Debug.Log($"Item: {item}");
            Debug.Log($"Modifier: {item.Modifier.GetType().Name}");
            Debug.Log($"Sprite: {item.Sprite.name}"); 
            
            Debug.Log("----------Stats----------");
        
            foreach (var itemStat in item.Stats)
            {
                Debug.Log(itemStat.GetType().Name);
            }
        
            Debug.Log("------------------------------");
        }
    }
}