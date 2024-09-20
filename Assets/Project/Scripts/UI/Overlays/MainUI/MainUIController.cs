using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts.UI.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        [Inject] private SpellsSpriteContainerSO _spellsSpriteContainerSO;
        
        private SummonSpellButton _firstSummonSpellButton;
        private SummonSpellButton _secondSummonSpellButton;
        private SummonSpellButton _thirdSummonSpellButton;
        private SummonSpellButton _fourthSummonSpellButton;
        
        private PlayerSpellButton _firstPlayerSpellButton;
        private PlayerSpellButton _secondPlayerSpellButton;
        private PlayerSpellButton _thirdPlayerSpellButton;
        private PlayerSpellButton _fourthPlayerSpellButton;

        protected override void Initialize()
        {
            SetSpellsSprites();
        }

        private void SetSpellsSprites()
        {
            _firstSummonSpellButton = Panel.FirstSummonSpellButton;
            _secondSummonSpellButton = Panel.SecondSummonSpellButton;
            _thirdSummonSpellButton = Panel.ThirdSummonSpellButton;
            _fourthSummonSpellButton = Panel.FourthSummonSpellButton;

            _firstPlayerSpellButton = Panel.FirstPlayerSpellButton;
            _secondPlayerSpellButton = Panel.SecondPlayerSpellButton;
            _thirdPlayerSpellButton = Panel.ThirdPlayerSpellButton;
            _fourthPlayerSpellButton = Panel.FourthPlayerSpellButton;
            
            _firstSummonSpellButton.Image.sprite = _spellsSpriteContainerSO.GetSummonImage("SS_0");
            _secondSummonSpellButton.Image.sprite = _spellsSpriteContainerSO.GetSummonImage("SS_1");
            _thirdSummonSpellButton.Image.sprite = _spellsSpriteContainerSO.GetSummonImage("SS_2");
            _fourthSummonSpellButton.Image.sprite = _spellsSpriteContainerSO.GetSummonImage("SS_3");

            _firstPlayerSpellButton.Image.sprite = _spellsSpriteContainerSO.GetPlayerImage("PS_0");
            _secondPlayerSpellButton.Image.sprite = _spellsSpriteContainerSO.GetPlayerImage("PS_1");
            _thirdPlayerSpellButton.Image.sprite = _spellsSpriteContainerSO.GetPlayerImage("PS_2");
            _fourthPlayerSpellButton.Image.sprite = _spellsSpriteContainerSO.GetPlayerImage("PS_3"); 
        }
    }
}