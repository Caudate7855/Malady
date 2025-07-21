using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts
{
    public class SpellList : MonoBehaviour
    {
        public List<SpellUIButtonBase> SpellUIButtonBase => _spellUIButtonBase;
        
        [SerializeField] private SpellType _spellsType;
        [SerializeField] private List<SpellUIButtonBase> _spellUIButtonBase = new();
        private List<SpellSo> _spellList = new();


        public void SetSpell(SpellSo spellToSet, int indexToSet)
        {
            if(spellToSet.Type != _spellsType)
            {
                return;
            }
            
            RemoveSpell(indexToSet);
            _spellList[indexToSet] = spellToSet;
            _spellUIButtonBase[indexToSet].SetSpellInfo(spellToSet);
        }
        
        private void RemoveSpell(int indexToRemove)
        {
            _spellList[indexToRemove] = null;
        }
    }
}