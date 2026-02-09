using System.Collections.Generic;
using Itibsoft.PanelManager;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class BookSpellListView : PanelBase
    {
        public Button CloseButton => _closeButton;
        public Button PassivePerksButton => _passivePerksButton;
        public Button MemoriesButton => _memoriesButton;

        public List<SpellSlot> BloodSpellSlots => _bloodSpellSlots;
        public List<SpellSlot> SoulSpellSlots => _soulsSpellSlots;
        public List<SpellSlot> BonesSpellSlots => _bonesSpellSlots;
        public List<SpellSlot> FleshSpellSlots => _fleshSpellSlots;
        
        [Title("Buttons", bold: true)]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _passivePerksButton;
        [SerializeField] private Button _memoriesButton;
        
        [Title("Lists", bold: true)]
        [SerializeField] private List<SpellSlot> _bloodSpellSlots;
        [SerializeField] private List<SpellSlot> _soulsSpellSlots;
        [SerializeField] private List<SpellSlot> _bonesSpellSlots;
        [SerializeField] private List<SpellSlot> _fleshSpellSlots;
    }
}