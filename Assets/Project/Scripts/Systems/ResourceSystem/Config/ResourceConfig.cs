using System;
using System.Collections.Generic;
using Project.Scripts.Configs;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = nameof(ResourceConfig), menuName = "Configs/" + nameof(ResourceConfig))]
    public class ResourceConfig : SerializedScriptableObject
    {
        [OdinSerialize] public List<Resource> ResourceConfigs { get; private set; } = new();

        public Resource GetResourceConfig(ResourceType resourceType)
        {
            for (var i = 0; i < ResourceConfigs.Count; i++)
            {
                if (ResourceConfigs[i].ResourceType == resourceType)
                {
                    return ResourceConfigs[i];
                }
            }

            throw new Exception($"Resource {resourceType.ToString()} not found");
        }
    }

    public struct Resource
    {
        [FoldoutGroup("@Name")]
        [HorizontalGroup("@Name/Horizontal", 60)]
        [PreviewField(60, ObjectFieldAlignment.Left)]
        [HideLabel]
        [Title("Icon", bold: true, horizontalLine: false)]
        public Sprite Icon;
        
        [FoldoutGroup("@Name")] 
        [VerticalGroup("@Name/Horizontal/Vertical")]
        [HideLabel]
        [Title("Name", bold: true, horizontalLine: false)]
        public string Name;
        
        [FoldoutGroup("@Name")] 
        [VerticalGroup("@Name/Horizontal/Vertical")]
        [HideLabel]
        [Title("Type", bold: true, horizontalLine: false)]
        [Space(-10)]
        public ResourceType ResourceType;
    }
}