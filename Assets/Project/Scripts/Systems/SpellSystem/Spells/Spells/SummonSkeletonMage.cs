using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class SummonSkeletonMage : SpellBase
    {
        public SummonSkeletonMage(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            
        }
    }
}