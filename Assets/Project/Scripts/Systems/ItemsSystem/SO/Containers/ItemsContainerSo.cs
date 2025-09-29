using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "ItemsContainer", menuName = "ItemSystems/ItemsContainer")]
    public class ItemsContainerSo : ScriptableObject
    {
        public List<ArmorItemSO> ArmorItems;
        public List<WeaponItemSO> WeaponsItems;
        public List<RingItemSO> RingItems;
    }
}