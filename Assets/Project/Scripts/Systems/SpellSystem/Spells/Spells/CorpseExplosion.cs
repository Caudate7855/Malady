using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class CorpseExplosion : SpellBase
    {
        public CorpseExplosion(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            
        }
    }
}