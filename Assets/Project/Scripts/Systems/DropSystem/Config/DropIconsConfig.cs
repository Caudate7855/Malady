using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = nameof(DropIconsConfig), menuName = "Configs/" + nameof(DropIconsConfig))]
    public class DropIconsConfig : ScriptableObject
    {
        public List<DropIconData> DropItemConfigs = new();
        
        public Sprite GetItemDropIcon(ItemType itemType)
        {
            for (var i = 0; i < DropItemConfigs.Count; i++)
            {
                if (DropItemConfigs[i].Type == itemType)
                {
                    return DropItemConfigs[i].Sprite;
                }
            }

            throw new Exception($"Item with type {itemType} not found");
        }
    }

    [Serializable]
    public struct DropIconData
    {
        [HorizontalGroup("@Horizontal", 50)]
        [HideLabel]
        [PreviewField(50, ObjectFieldAlignment.Left)]
        [Title("Sprite", bold: true, horizontalLine: false)]
        public Sprite Sprite;

        [HorizontalGroup("@Horizontal")]
        [HideLabel]
        [Title("Type", bold: true, horizontalLine: false)]
        public ItemType Type;
    }
}