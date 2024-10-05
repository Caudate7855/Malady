using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class ItemsBaseSO : ScriptableObject
    {
        public string ItemName;
        
        public ItemType ItemType;
        public ItemRarityType ItemRarityType;

        public Sprite BackgroundImage;
        public Sprite ItemImage;
        
        public float Price;
        
        public string Description;
    }
}