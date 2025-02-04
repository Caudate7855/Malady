using System.Collections.Generic;
using Itibsoft.PanelManager;
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

        private List<SpellUIButtonBase> _bloodSpellsList;
        private List<SpellUIButtonBase> _soulsSpellsList;
        private List<SpellUIButtonBase> _bonesSpellsList;
        private List<SpellUIButtonBase> _fleshSpellsList;

        private Button _closeButton;

        private bool _isTipSetted;

        protected override void Initialize()
        {
            _bloodSpellsList = Panel.BloodSpellsList;
            _soulsSpellsList = Panel.SoulsSpellsList;
            _bonesSpellsList = Panel.BonesSpellsList;
            _fleshSpellsList = Panel.FleshSpellsList;

            _closeButton = Panel.CloseButton;

            _closeButton.onClick.AddListener(Close);
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
        
        private void SetSpells()
        {
            SetSpell(_bloodSpellsList, 0, "ps_0"); 
            SetSpell(_bloodSpellsList, 5, "ps_1"); 
            SetSpell(_fleshSpellsList, 0, "ps_2"); 
            SetSpell(_bonesSpellsList, 0, "ps_3"); 
            SetSpell(_soulsSpellsList, 0, "ps_4"); 
        }

        private void SetSpell(List<SpellUIButtonBase> spellList, int spellIndex, string spellID)
        {
            spellList[spellIndex].SetSpellInfo(_spellsContainerSo.GetSpell(spellID));
        }
    }
}