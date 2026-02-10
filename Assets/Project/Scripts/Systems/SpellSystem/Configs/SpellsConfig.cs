using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Player;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.Scripts.Configs
{
    [CreateAssetMenu(fileName = nameof(SpellsConfig), menuName = "Configs/" + nameof(SpellsConfig))]
    public sealed class SpellsConfig : SerializedScriptableObject
    {
        [OdinSerialize] public List<SpellConfigWrapper> SpellConfigsWrappers { get; private set; } = new();

        public SpellConfig GetSpellConfig(Type spellType)
        {
            for (var i = 0; i < SpellConfigsWrappers.Count; i++)
            {
                for (int j = 0; j < SpellConfigsWrappers[i].SpellConfigs.Count; j++)
                {
                    if (SpellConfigsWrappers[i].SpellConfigs[j].Type.GetType() == spellType)
                    {
                        return SpellConfigsWrappers[i].SpellConfigs[j];
                    }
                }
            }

            throw new Exception($"Spell {spellType.Name} not found");
        }
    }

    [Serializable]
    public class SpellConfigWrapper
    {
        [FoldoutGroup("@ElementType")]
        public SpellElementType ElementType;

        [OdinSerialize]
        [FoldoutGroup("@ElementType")]
        public List<SpellConfig> SpellConfigs { get; private set; } = new();
    }

    [Serializable]
    public sealed class SpellConfig
    {
        [FoldoutGroup("@Name")]
        [HorizontalGroup("@Name/Horizontal", 100)]
        [PreviewField(100, ObjectFieldAlignment.Left)]
        [Title("Icon", horizontalLine: false)]
        [HideLabel]
        public Sprite Icon;
        
        [FoldoutGroup("@Name")]
        [VerticalGroup("@Name/Horizontal/Vertical")]
        [Title("Name", horizontalLine: false)]
        [HideLabel]
        public string Name;

        [FoldoutGroup("@Name")]
        [VerticalGroup("@Name/Horizontal/Vertical")]
        [Title("Description", horizontalLine: false)]
        [TextArea(3, 20)]
        [HideLabel] 
        public string Description;

        [FoldoutGroup("@Name")]
        [OdinSerialize]
        [TypeFilter(nameof(GetFilteredTypeList))]
        public SpellBase Type;
        
        [FoldoutGroup("@Name")]
        public SpellElementType ElementType;

        [FoldoutGroup("@Name")]
        public SpellViewBase SpellView;

        [FoldoutGroup("@Name")] 
        public PlayerCastAnimationType AnimationType;
        
        [FoldoutGroup("@Name")]
        [DictionaryDrawerSettings(KeyLabel = "Resource type", ValueLabel = "Count")]
        public Dictionary<ResourceType, float> Cost;

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