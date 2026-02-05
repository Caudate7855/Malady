using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class BoneArmor : SpellBase
    {
        public BoneArmor(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            
        }
    }
}