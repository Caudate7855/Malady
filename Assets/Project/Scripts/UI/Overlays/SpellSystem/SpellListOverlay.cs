using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Overlays
{
    public class SpellListOverlay : PanelBase
    {
        public Button CloseButton => _closeButton;
        
        public List<SpellUIButtonBase> BloodSpellsList => _bloodSpellsList;
        public List<SpellUIButtonBase> SoulsSpellsList => _soulsSpellsList;
        public List<SpellUIButtonBase> BonesSpellsList => _bonesSpellsList;
        public List<SpellUIButtonBase> FleshSpellsList => _fleshSpellsList;

        [SerializeField] private Button _closeButton;
        [SerializeField] private List<SpellUIButtonBase> _bloodSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _soulsSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _bonesSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _fleshSpellsList;
    }
}