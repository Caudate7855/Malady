using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;

namespace Project.Scripts.Overlays
{
    public class SpellListOverlay : PanelBase
    {
        public List<SpellButtonBase> BloodSpellsList => _bloodSpellsList;
        public List<SpellButtonBase> SoulsSpellsList => _soulsSpellsList;
        public List<SpellButtonBase> BonesSpellsList => _bonesSpellsList;
        public List<SpellButtonBase> FleshSpellsList => _fleshSpellsList;
        
        [SerializeField] private List<SpellButtonBase> _bloodSpellsList;
        [SerializeField] private List<SpellButtonBase> _soulsSpellsList;
        [SerializeField] private List<SpellButtonBase> _bonesSpellsList;
        [SerializeField] private List<SpellButtonBase> _fleshSpellsList;
    }
}