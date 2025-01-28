using System.Collections.Generic;
using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        public List<SpellButtonBase> SummonSpellButtons;
        public List<SpellButtonBase> PayerSpellButtons;
        
        [Inject] private SpellsSpriteContainerSO _spellsSpriteContainerSO;
        [Inject] private SpellTipHandler _spellTipHandler;
        
        private HpBar _hpBar;
        private EssenceBar _essenceBar;
        private BossBar _bossBar;
        private List<ElementalBarBase> _elementalSliders;

        protected override void Initialize()
        {
            PayerSpellButtons = Panel.PlayerSpellButton;
            SummonSpellButtons = Panel.SummonSpellsButtons;
            
            SetBars();
            SetSpellsSprites();
            SetSpellTip(SummonSpellButtons);
            SetSpellTip(PayerSpellButtons);
        }


        private void SetSpellTip(List<SpellButtonBase> list)
        {
            foreach (var spell in list)
            {
                spell.SetSpellTipHandler(_spellTipHandler);
            }
        }

        private void SetBars()
        {
            _hpBar = Panel.HpSlider;
            _essenceBar = Panel.EssenceSlider;
            _bossBar = Panel.BossBar;
            _elementalSliders = Panel.ElementalBars;
        }

        private void SetSpellsSprites()
        {
            for (int i = 0, count = SummonSpellButtons.Count; i < count; i++)
            {
                PayerSpellButtons[i].UpdateImage(_spellsSpriteContainerSO.GetPlayerImage($"PS_{i}"));
            }

            for (int i = 0, count = SummonSpellButtons.Count; i < count; i++)
            {
                SummonSpellButtons[i].UpdateImage(_spellsSpriteContainerSO.GetSummonImage($"SS_{i}"));
                
            }
        }

        public void OnPlayerSpellButtonClicked(int index)
        {
            PayerSpellButtons[index].Interact();
        }
        
        public void OnSummonSpellButtonClicked(int index)
        {
            SummonSpellButtons[index].Interact();
        }
    }
}