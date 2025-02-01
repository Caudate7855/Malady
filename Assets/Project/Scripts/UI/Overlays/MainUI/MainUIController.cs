using System.Collections.Generic;
using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        private List<SpellButtonBase> _summonSpellButtons;
        private List<SpellButtonBase> _payerSpellButtons;
        
        [Inject] private SpellTipHandler _spellTipHandler;
        [Inject] private SpellsContainerSo _spellsContainerSo;
        
        protected override void Initialize()
        {
            _payerSpellButtons = Panel.PlayerSpellButton;
            _summonSpellButtons = Panel.SummonSpellsButtons;
            
            SetSpellsSprites();
            SetSpellTip(_summonSpellButtons);
            SetSpellTip(_payerSpellButtons);
        }


        private void SetSpellTip(List<SpellButtonBase> list)
        {
            foreach (var spell in list)
            {
                spell.SetSpellTipHandler(_spellTipHandler);
            }
        }

        private void SetSpellsSprites()
        {
            _payerSpellButtons[0].SetSpellInfo(_spellsContainerSo.GetSpell($"ps_1"));
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