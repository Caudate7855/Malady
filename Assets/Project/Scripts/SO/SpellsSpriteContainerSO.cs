using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    [CreateAssetMenu(fileName = "SpellSpriteContainer", menuName = "ImageContainers/SpellSpriteContainer", order = 0)]
    public class SpellsSpriteContainerSO : ScriptableObject
    {
        [SerializeField] public List<SpellsImages> _playerSpellsSprites;
        [SerializeField] public List<SpellsImages> _summonsSpellsSprites;

        public Sprite GetPlayerImage(string spellID)
        {
            for (int i = 0, count = _playerSpellsSprites.Count; i < count; i++)
            {
                if (_playerSpellsSprites[i].SpellID == spellID)
                {
                    return _playerSpellsSprites[i].Sprite;
                }
            }

            return default;
        }
        
        public Sprite GetSummonImage(string spellID)
        {
            for (int i = 0, count = _summonsSpellsSprites.Count; i < count; i++)
            {
                if (_summonsSpellsSprites[i].SpellID == spellID)
                {
                    return _summonsSpellsSprites[i].Sprite;
                }
            }

            return default;
        }
        
        [Serializable]
        public class SpellsImages
        {
            public string SpellID;
            public Sprite Sprite;
        }
    }
}