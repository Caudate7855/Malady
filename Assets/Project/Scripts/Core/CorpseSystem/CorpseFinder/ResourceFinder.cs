using System.Collections.Generic;
using JetBrains.Annotations;
using Project.Scripts.CorpseSystem.Resource.Abstracts;
using UnityEngine;

namespace Project.Scripts.CorpseSystem
{
    [UsedImplicitly]
    public class ResourceFinder
    {
        public List<CorpseResourceObjectBase> GetCorpseResourceInRadius<T>(Vector3 initialPosition, float distance, int resourcesCount) where T : CorpseResourceObjectBase
        {
            List<CorpseResourceObjectBase> foundedResourcesInDistance = new();
            List<CorpseResourceObjectBase> correctResourcesInDistance = new();
            
            var colliders = Physics.OverlapSphere(initialPosition, distance);
            
            if (colliders.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < colliders.Length; i++)
            {
                var resourcesOnCorpse = colliders[i].GetComponentsInChildren<CorpseResourceObjectBase>();

                for (int j = 0; j < resourcesOnCorpse.Length; j++)
                {
                    foundedResourcesInDistance.Add(resourcesOnCorpse[j]);
                }
            }

            if (foundedResourcesInDistance.Count == 0)
            {
                return null;
            }
            
            for (int i = 0; i < foundedResourcesInDistance.Count; i++)
            {
                if (foundedResourcesInDistance[i].GetType() == typeof(T))
                {
                    if (i <= resourcesCount)
                    {
                        correctResourcesInDistance.Add(foundedResourcesInDistance[i]);
                    }
                }
            }

            return correctResourcesInDistance;
        }
    }
}