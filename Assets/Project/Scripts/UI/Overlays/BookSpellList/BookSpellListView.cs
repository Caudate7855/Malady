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

        public List<SpellUIButton> BloodUIButtons => _bloodUIButtons;
        public List<SpellUIButton> SoulUIButtons => _soulsUIButtons;
        public List<SpellUIButton> BonesUIButtons => _bonesUIButtons;
        public List<SpellUIButton> FleshUIButtons => _fleshUIButtons;
        
        [Title("Buttons", bold: true)]
        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _passivePerksButton;
        [SerializeField] private Button _memoriesButton;
        
        [Title("Lists", bold: true)]
        [SerializeField] private List<SpellUIButton> _bloodUIButtons;
        [SerializeField] private List<SpellUIButton> _soulsUIButtons;
        [SerializeField] private List<SpellUIButton> _bonesUIButtons;
        [SerializeField] private List<SpellUIButton> _fleshUIButtons;
    }
}