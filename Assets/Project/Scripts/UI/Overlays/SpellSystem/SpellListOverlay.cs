using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class SpellListOverlay : PanelBase
    {
        public Button CloseButton => _closeButton;
        
        public List<SpellUIButtonBase> BloodSpellsList => _bloodSpellsList;
        public List<SpellUIButtonBase> SoulsSpellsList => _soulsSpellsList;
        public List<SpellUIButtonBase> BonesSpellsList => _bonesSpellsList;
        public List<SpellUIButtonBase> FleshSpellsList => _fleshSpellsList;
        public Button PassivePerksButton => _passivePerksButton;
        public Button MemoriesPerksButton => _memoriesPerksButton;

        [SerializeField] private Button _closeButton;
        [SerializeField] private List<SpellUIButtonBase> _bloodSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _soulsSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _bonesSpellsList;
        [SerializeField] private List<SpellUIButtonBase> _fleshSpellsList;
        [SerializeField] private Button _passivePerksButton;
        [SerializeField] private Button _memoriesPerksButton;
    }
}