using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class BoneSpear : SpellBase
    {
        public BoneSpear(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
        
        }
    }
}