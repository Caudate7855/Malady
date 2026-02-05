using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public sealed class BoneSpear : SpellBase
    {
        public BoneSpear(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
        
        }
    }
}