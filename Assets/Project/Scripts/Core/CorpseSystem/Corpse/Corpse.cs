using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class Corpse : MonoBehaviour
    {
        [SerializeField] private CorpseSoul _soul;
        [SerializeField] private CorpseFlesh _flesh;
        [SerializeField] private CorpseBlood _blood;
        [SerializeField] private CorpseBones _bones;

        private Dictionary<ResourceType, CorpseResource> _resourcesObjects;

        ResourceFinder _resourceFinder = new ();
        
        private void Awake()
        {
            InitializeResources();
        }

        private void Start()
        {
            var resource = _resourceFinder.GetAllCorpseResources<CorpseSoul>(transform.position, 10f);
            
            resource.SwitchState(false);
        }

        private void InitializeResources()
        {
            _resourcesObjects = new()
            {
                { ResourceType.Souls, new CorpseResource(ResourceType.Souls, _soul) },
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

        public void RemoveAllResources()
        {
            foreach (var corpseResource in _resourcesObjects)
            {
                RemoveResource(corpseResource.Key);
            }
        }

        public void Explode()
        {
            
        }
    }
}