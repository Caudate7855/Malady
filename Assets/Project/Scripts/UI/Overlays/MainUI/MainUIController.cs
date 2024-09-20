using System.Collections.Generic;
using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts.UI.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        [Inject] private SpellsSpriteContainerSO _spellsSpriteContainerSO;
        
        public List<SummonSpellButton> SummonSpellButtons;
        public List<PlayerSpellButton> PayerSpellButtons;

        protected override void Initialize()
        {
            SetSpellsSprites();
        }

        private void SetSpellsSprites()
        {
            PayerSpellButtons = Panel.PlayerSpellButton;
            SummonSpellButtons = Panel.SummonSpellsButtons;

            for (int i = 0, count = SummonSpellButtons.Count; i < count; i++)
            {
                PayerSpellButtons[i].Image.sprite = _spellsSpriteContainerSO.GetPlayerImage($"PS_{i}");
            }

            for (int i = 0, count = SummonSpellButtons.Count; i < count; i++)
            {
                SummonSpellButtons[i].Image.sprite = _spellsSpriteContainerSO.GetSummonImage($"SS_{i}");
            }
        }

        private void OnPlayerSpellButtonClicked(int index)
        {
            PayerSpellButtons[index].Interact();
        }
        
        private void OnSummonSpellButtonClicked(int index)
        {
            SummonSpellButtons[index].Interact();
        }
    }
}