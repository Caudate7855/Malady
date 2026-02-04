using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class Corpse : MonoBehaviour
    {
        [SerializeField] private CorpseSoul _soul;
        [SerializeField] private CorpseFlesh _flesh;
        [SerializeField] private CorpseBlood _blood;
        [SerializeField] private CorpseBones _bones;

        private Dictionary<ResourceType, CorpseResource> _resourcesObjects;

        private void Awake()
        {
            InitializeResources();
        }

        private void InitializeResources()
        {
            _resourcesObjects = new()
            {
                { ResourceType.Soul, new CorpseResource(ResourceType.Soul, _soul) },
                { ResourceType.Flesh, new CorpseResource(ResourceType.Flesh, _flesh) },
                { ResourceType.Blood, new CorpseResource(ResourceType.Blood, _blood) },
                { ResourceType.Bones, new CorpseResource(ResourceType.Bones, _bones) }
            };
        }

        public void RemoveResource(ResourceType resourceType)
        {
            _resourcesObjects[resourceType].HasResource = false;
            _resourcesObjects[resourceType].ResourceObject.SwitchState(false);
        }
    }
}