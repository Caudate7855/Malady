using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class SoulMage : SpellBase
    {
        public SoulMage(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
        }
    }
}