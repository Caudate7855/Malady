using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "ItemsContainer", menuName = "Items/ItemsContainer")]
    public class ItemsContainerSO : ScriptableObject
    {
        public ItemType ItemType;
        
        public string ItemName;
        
        public Sprite BackgroundImage;
        public Sprite ItemImage;
        
        public string Description;
    }
}