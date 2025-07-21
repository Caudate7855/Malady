using System.Collections.Generic;
using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        private SpellList _summonSpellList;
        private SpellList _playerSpellList;
        
        [Inject] private SpellTipHandler _spellTipHandler;
        [Inject] private SpellDragImageHandler _spellDragImageHandler;
        [Inject] private SpellsContainerSo _spellsContainerSo;
        
        protected override void Initialize()
        {
            _summonSpellList = Panel.SummonSpellList;
            _playerSpellList = Panel.PlayerSpellList;

            SetSpellTip(_summonSpellList.SpellUIButtonBase);
            SetSpellTip(_playerSpellList.SpellUIButtonBase);
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
            _playerSpellList.SpellUIButtonBase[index].SetSpellInfo(_spellsContainerSo.GetSpell(type, row, column));
        }

        public void OnPlayerSpellButtonClicked(int index)
        {
            _playerSpellList.SpellUIButtonBase[index].Interact();
        }
        
        public void OnSummonSpellButtonClicked(int index)
        {
            _summonSpellList.SpellUIButtonBase[index].Interact();
        }
    }
}