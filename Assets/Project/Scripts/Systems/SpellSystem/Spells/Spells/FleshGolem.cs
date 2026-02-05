using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class FleshGolem : SpellBase
    {
        public FleshGolem(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            
        }
    }
}