using System;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public class SpellModificatorConfig
    {
        public string ID;
        public string SpellName;
        public string SpellModificatorName;
        [TextArea (5,1)]public string Description;
    }
}