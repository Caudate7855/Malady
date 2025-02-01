using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;

namespace Project.Scripts.Overlays
{
    public class SpellListOverlay : PanelBase
    {
        public List<SpellUIButtonBase> BloodSpellsList => _bloodSpellsList;
        public List<SpellUIButtonBase> SoulsSpellsList => _soulsSpellsList;
        public List<SpellUIButtonBase> BonesSpellsList => _bonesSpellsList;
        public List<SpellUIButtonBase> FleshSpellsList => _fleshSpellsList;
        
        [SerializeField] private List<SpellUIButtonBase> _bloodSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _soulsSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _bonesSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _fleshSpellsList;
    }
}