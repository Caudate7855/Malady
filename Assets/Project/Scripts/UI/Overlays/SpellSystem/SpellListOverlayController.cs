using System.Collections.Generic;
using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "SpellListOverlay")]
    public class SpellListOverlayController : PanelControllerBase<SpellListOverlay>
    {
        [Inject] private SpellTipHandler _spellTipHandler;
        [Inject] private SpellsContainerSo _spellsContainerSo;
        
        private List<SpellUIButtonBase> _bloodSpellsList;
        private List<SpellUIButtonBase> _soulsSpellsList;
        private List<SpellUIButtonBase> _bonesSpellsList;
        private List<SpellUIButtonBase> _fleshSpellsList;

        private bool _isTipSetted;
        
        protected override void Initialize()
        {
            _bloodSpellsList = Panel.BloodSpellsList;
            _soulsSpellsList = Panel.SoulsSpellsList;
            _bonesSpellsList = Panel.BonesSpellsList;
            _fleshSpellsList = Panel.FleshSpellsList;
        }

        protected override void OnOpenBefore()
        {
            if (!_isTipSetted)
            {
                for (int i = 0; i < _bloodSpellsList.Count; i++)
                {
                    _bloodSpellsList[i].SetSpellTipHandler(_spellTipHandler);
                }
                
                for (int i = 0; i < _soulsSpellsList.Count; i++)
                {
                    _bloodSpellsList[i].SetSpellTipHandler(_spellTipHandler);
                }
                
                for (int i = 0; i < _bonesSpellsList.Count; i++)
                {
                    _bloodSpellsList[i].SetSpellTipHandler(_spellTipHandler);
                }
                
                for (int i = 0; i < _fleshSpellsList.Count; i++)
                {
                    _bloodSpellsList[i].SetSpellTipHandler(_spellTipHandler);
                }
            }

            SetSpell(_bloodSpellsList, 0);
        }
        
        private void SetSpell(List<SpellUIButtonBase> spellList,int spellIndex)
        {
            spellList[spellIndex].SetSpellInfo(_spellsContainerSo.GetSpell($"ps_1"));
        }
    }
}