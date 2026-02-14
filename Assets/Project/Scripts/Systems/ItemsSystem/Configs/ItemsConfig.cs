using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = nameof(ItemsConfig), menuName = "Configs/" + nameof(ItemsConfig))]
    public sealed class ItemsConfig : SerializedScriptableObject
    {
        [OdinSerialize] public List<ItemConfig> ItemsConfigs { get; private set; } = new();

        public ItemConfig GetItemConfigByType(ItemType itemType)
        {
            for (var i = 0; i < ItemsConfigs.Count; i++)
            {
                if (ItemsConfigs[i].Type == itemType)
                {
                    return ItemsConfigs[i];
                }
            }

            throw new Exception($"Item with type {itemType} not found");
        }
    }

    [Serializable]
    public class ItemConfig
    {
        [FoldoutGroup("@Type")]
        [HorizontalGroup("@Type/Horizontal", 100)]
        [HideLabel]
        [PreviewField(100, ObjectFieldAlignment.Left)]
        [Title("Drop sprite", horizontalLine: false)]
        public Sprite DropSprite;
        
        [FoldoutGroup("@Type")]
        [VerticalGroup("@Type/Horizontal/Right")]
        [HideLabel]
        [Title("Item type", bold: true, horizontalLine: false)]
        public ItemType Type;
        
        [FoldoutGroup("@Type")]
        [HorizontalGroup("@Type/Horizontal/Right/Left", MarginLeft = 4)]
        [HideLabel]
        [Title("MinStatsCount", bold: true, horizontalLine: false)]
        public int MinStatsCount;
        
        [FoldoutGroup("@Type")]
        [HorizontalGroup("@Type/Horizontal/Right/Left", MarginLeft = 4)]
        [HideLabel]
        [Title("MaxStatsCount", bold: true, horizontalLine: false)]
        public int MaxStatsCount;
        
        [FoldoutGroup("@Type")]
        [LabelText("Stats variants")]
        public List<PossibleStat> PossibleStat;
        
        [FoldoutGroup("@Type")]
        [LabelText("Modifs variants")]
        public List<PossibleModif> PossibleModifs;
        
        [FoldoutGroup("@Type")]
        [PreviewField(150, ObjectFieldAlignment.Center)]
        public List<Sprite> Sprites;
    }

    [Serializable]
    public struct PossibleStat
    {
        [ShowInInspector]
        [TypeFilter(nameof(GetFilteredTypeList))]
        [HideLabel]
        public StatBase Stat;
        
        private IEnumerable<Type> GetFilteredTypeList()
        {
            var type = typeof(StatBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(StatBase).IsAssignableFrom(x));

            return type;
        }
    }
    
    [Serializable]
    public struct PossibleModif
    {
        [ShowInInspector]
        [TypeFilter(nameof(GetFilteredTypeList))]
        [HideLabel]
        public ISpellModifier Modifier;
        
        private IEnumerable<Type> GetFilteredTypeList()
        {
            var type = typeof(ISpellModifier).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(ISpellModifier).IsAssignableFrom(x));

            return type;
        }
    }
}