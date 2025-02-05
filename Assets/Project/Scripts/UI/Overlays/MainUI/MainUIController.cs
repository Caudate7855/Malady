using System.Collections.Generic;
using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        private List<SpellUIButtonBase> _summonSpellButtons;
        private List<SpellUIButtonBase> _payerSpellButtons;
        
        [Inject] private SpellTipHandler _spellTipHandler;
        [Inject] private SpellDragImageHandler _spellDragImageHandler;
        [Inject] private SpellsContainerSo _spellsContainerSo;
        
        protected override void Initialize()
        {
            _payerSpellButtons = Panel.PlayerSpellButton;
            _summonSpellButtons = Panel.SummonSpellsButtons;

            SetSpellTip(_summonSpellButtons);
            SetSpellTip(_payerSpellButtons);
        }


        private void SetSpellTip(List<SpellUIButtonBase> list)
        {
            foreach (var spell in list)
            {
                spell.SetSpellHandlers(_spellTipHandler, _spellDragImageHandler);
            }
        }

        private void SetSpell(int index, SpellElementType type, int row, int column)
        {
            _payerSpellButtons[index].SetSpellInfo(_spellsContainerSo.GetSpell(type, row, column));
        }

        public void OnPlayerSpellButtonClicked(int index)
        {
            _payerSpellButtons[index].Interact();
        }
        
        public void OnSummonSpellButtonClicked(int index)
        {
            _summonSpellButtons[index].Interact();
        }
    }
}