using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = nameof(SpellsConfig), menuName = "Configs/" + nameof(SpellsConfig))]
    public sealed class SpellsConfig : SerializedScriptableObject
    {
        [OdinSerialize] public List<SpellConfig> SpellConfigs { get; private set; } = new();

        public SpellConfig GetSpellConfig(Type spellType)
        {
            for (var i = 0; i < SpellConfigs.Count; i++)
            {
                if (SpellConfigs[i].Type.GetType() == spellType)
                {
                    return SpellConfigs[i];
                }
            }

            throw new Exception($"Spell {spellType.Name} not found");
        }
    }

    [Serializable]
    public struct SpellConfig
    {
        [FoldoutGroup("@Name")]
        public string Name;

        [FoldoutGroup("@Name")]
        [TextArea(2, 20)]
        public string Description;

        [FoldoutGroup("@Name")]
        [ShowInInspector]
        [TypeFilter(nameof(GetFilteredTypeList))]
        public SpellBase Type;

        [FoldoutGroup("@Name")]
        public SpellViewBase SpellView;

        [VerticalGroup("@Name/Vertical")]
        [HorizontalGroup("@Name/Vertical/HorizontalGroup_1", width: 120)]
        [ToggleLeft]
        [LabelText("LifeTime")]
        public bool HasLifeTime;

        [HorizontalGroup("@Name/Vertical/HorizontalGroup_1")]
        [ShowIf("HasLifeTime")]
        [HideLabel]
        public float LifeTime;

        [HorizontalGroup("@Name/Vertical/HorizontalGroup_2", width: 120)]
        [ToggleLeft]
        [LabelText("Speed")]
        public bool HasSpeed;

        [HorizontalGroup("@Name/Vertical/HorizontalGroup_2")]
        [ShowIf("HasSpeed")]
        [HideLabel]
        public float Speed;

        private IEnumerable<Type> GetFilteredTypeList()
        {
            var type = typeof(SpellBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(SpellBase).IsAssignableFrom(x));

            return type;
        }
    }
}