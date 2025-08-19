using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.SkillTree;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "SpellListOverlay")]
    public class SpellListOverlayController : PanelControllerBase<SpellListOverlay>
    {
        [Inject] private SpellTipHandler _spellTipHandler;
        [Inject] private SpellDragImageHandler _spellDragImageHandler;
        [Inject] private SpellsContainerSo _spellsContainerSo;
        [Inject] private IPanelManager _panelManager;

        private List<SpellUIButtonBase> _bloodSpellsList;
        private List<SpellUIButtonBase> _soulsSpellsList;
        private List<SpellUIButtonBase> _bonesSpellsList;
        private List<SpellUIButtonBase> _fleshSpellsList;
        
        private Button _passivePerksButton;
        private Button _memoriesPerksButton;
        private Button _closeButton;

        private bool _isTipSetted;

        private SkillTreeOverlayController _skillTreeOverlayController;

        protected override void Initialize()
        {
            _bloodSpellsList = Panel.BloodSpellsList;
            _soulsSpellsList = Panel.SoulsSpellsList;
            _bonesSpellsList = Panel.BonesSpellsList;
            _fleshSpellsList = Panel.FleshSpellsList;
            
            _passivePerksButton = Panel.PassivePerksButton;
            _memoriesPerksButton = Panel.MemoriesPerksButton;
            _closeButton = Panel.CloseButton;

            _passivePerksButton.onClick.AddListener(OnPassivePerksButtonPressed);
            _memoriesPerksButton.onClick.AddListener(OnMemoriesButtonPressed);
            _closeButton.onClick.AddListener(Close);
            
            _skillTreeOverlayController =  _panelManager.LoadPanel<SkillTreeOverlayController>();
        }

        protected override void OnOpenBefore()
        {
            if (!_isTipSetted)
            {
                for (int i = 0; i < _bloodSpellsList.Count; i++)
                {
                    _bloodSpellsList[i].SetSpellHandlers(_spellTipHandler, _spellDragImageHandler);
                }

                for (int i = 0; i < _soulsSpellsList.Count; i++)
                {
                    _soulsSpellsList[i].SetSpellHandlers(_spellTipHandler, _spellDragImageHandler);
                }

                for (int i = 0; i < _bonesSpellsList.Count; i++)
                {
                    _bonesSpellsList[i].SetSpellHandlers(_spellTipHandler, _spellDragImageHandler);
                }

                for (int i = 0; i < _fleshSpellsList.Count; i++)
                {
                    _fleshSpellsList[i].SetSpellHandlers(_spellTipHandler, _spellDragImageHandler);
                }
            }

            _isTipSetted = true;

            SetSpells();
        }

        private void OnPassivePerksButtonPressed()
        {
            _skillTreeOverlayController.Open();
            Close();
        }
        
        private void OnMemoriesButtonPressed()
        {
            
        }
        
        private void SetSpells()
        {
            SetSpell(_bloodSpellsList, 0, 0); 
            SetSpell(_bloodSpellsList, 0, 1); 
            SetSpell(_fleshSpellsList, 0, 0); 
            SetSpell(_fleshSpellsList, 0, 1); 
            SetSpell(_bonesSpellsList, 0, 0); 
            SetSpell(_bonesSpellsList, 0, 1); 
            SetSpell(_soulsSpellsList, 0, 0); 
            SetSpell(_soulsSpellsList, 0, 1); 
        }

        private void SetSpell(List<SpellUIButtonBase> spellList, int row, int column)
        {
            var index = row;
            
            if (column > 0)
            {
                index += 5;
            }
            
            var spell = spellList[index];
            
            spell.SetSpellInfo(_spellsContainerSo.GetSpell(spell.GetSpellElementType(), row, column));
        }
    }
}