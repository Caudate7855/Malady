using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class EquipmentSystemTest : MonoBehaviour
    {
        [SerializeField] private ItemType _itemType;
        
        [Inject] private EquipmentSystem _equipmentSystem;
        [Inject] private ItemSystem _itemSystem;
        
        [Button]
        public void EquipItem()
        {
            var itemData = _itemSystem.CreateItemByType(_itemType);
            _equipmentSystem.EquipItem(itemData);
        }
        
        [Button]
        public void RemoveItem()
        {
            _equipmentSystem.RemoveItem(_itemType);
        }
    }
}