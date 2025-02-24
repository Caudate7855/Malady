using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class ResourceFinder
    {
        public CorpseResourceObjectBase GetAllCorpseResources<T>(Vector3 initialPosition, float distance) where T : CorpseResourceObjectBase
        {
            List<CorpseResourceObjectBase> corpseResourceObjectBase = new();
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
                    corpseResourceObjectBase.Add(resourcesOnCorpse[j]);
                }
            }

            if (corpseResourceObjectBase.Count == 0)
            {
                return null;
            }
            
            for (int i = 0; i < corpseResourceObjectBase.Count; i++)
            {
                if (corpseResourceObjectBase[i].GetType() == typeof(T))
                {
                    return corpseResourceObjectBase[i];
                }
            }

            return null;
        }
    }
}