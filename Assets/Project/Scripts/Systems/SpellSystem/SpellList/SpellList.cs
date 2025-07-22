using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts
{
    public class SpellList : MonoBehaviour
    {
        public List<SpellSo> ChosenSpells => _chosenSpells;
        public List<SpellUIButtonBase> SpellUIButtonBase => _spellUIButtonBase;

        public SpellType SpellsType;
        [SerializeField] private List<SpellUIButtonBase> _spellUIButtonBase = new();
        [SerializeField] private List<SpellSo> _chosenSpells = new();


        private void Awake()
        {
            for (int i = 0; i < SpellUIButtonBase.Count; i++)
            {
                _chosenSpells.Add(_spellUIButtonBase[i].Spell);
            }
        }

        public void SetSpell(SpellSo spellToSet, int indexToSet)
        {
            RemoveSpell(indexToSet); 
            _chosenSpells[indexToSet] = spellToSet;
            _spellUIButtonBase[indexToSet].Spell = spellToSet;
        }
        
        private void RemoveSpell(int indexToRemove)
        {
            _chosenSpells[indexToRemove] = null;
        }
    }
}