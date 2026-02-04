using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = nameof(StatsConfig), menuName = "Configs/" + nameof(StatsConfig))]
    public class StatsConfig : SerializedScriptableObject
    {
        [OdinSerialize] public List<StatConfig> StatsList { get; private set; } = new();

        public StatConfig GetStat<T>() where T : StatBase
        {
            for (var i = 0; i < StatsList.Count; i++)
            {
                if (StatsList[i].Type.GetType() == typeof(T))
                {
                    return StatsList[i];
                }
            }

            throw new Exception($"No stat with type {typeof(T).Name} in config");
        }
    }
    
    [Serializable]
    public struct StatConfig
    {
        [FoldoutGroup("@Name")]
        public string Name;

        [FoldoutGroup("@Name")]
        [ShowInInspector]
        [TypeFilter(nameof(GetFilteredTypeList))]
        public StatBase Type;

        [FoldoutGroup("@Name")]
        [TextArea(2, 20)]
        public string Description;

        [VerticalGroup("@Name/Vertical")]
        [HorizontalGroup("@Name/Vertical/HorizontalGroup_1", width: 120)]
        [ToggleLeft]
        [LabelText("InitValue")]
        public bool HasInitValue;

        [HorizontalGroup("@Name/Vertical/HorizontalGroup_1")]
        [ShowIf("HasInitValue")]
        [HideLabel]
        public float InitValue;

        [HorizontalGroup("@Name/Vertical/HorizontalGroup_2", width: 120)]
        [ToggleLeft]
        [LabelText("MinValue")]
        public bool HasMinValue;

        [HorizontalGroup("@Name/Vertical/HorizontalGroup_2")]
        [ShowIf("HasMinValue")]
        [HideLabel]
        public float MinValue;

        [HorizontalGroup("@Name/Vertical/HorizontalGroup_3", width: 120)]
        [ToggleLeft]
        [LabelText("MaxValue")]
        public bool HasMaxValue;

        [HorizontalGroup("@Name/Vertical/HorizontalGroup_3")]
        [ShowIf("HasMaxValue")]
        [HideLabel]
        public float MaxValue;

        private IEnumerable<Type> GetFilteredTypeList()
        {
            var type = typeof(StatBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(StatBase).IsAssignableFrom(x));

            return type;
        }
    }
}