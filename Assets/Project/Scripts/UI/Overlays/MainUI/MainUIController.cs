using System.Collections.Generic;
using Itibsoft.PanelManager;
using Zenject;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "MainUIView")]
    public class MainUIController : PanelControllerBase<MainUIView>
    {
        public SpellList SummonSpellList => _summonSpellList;
        public SpellList PlayerSpellList => _playerSpellList;

        private SpellList _summonSpellList;
        private SpellList _playerSpellList;

        [Inject] private SpellTipHandler _spellTipHandler;
        [Inject] private SpellDragImageHandler _spellDragImageHandler;
        [Inject] private SpellsContainerSo _spellsContainerSo;

        private List<BarBase> _bars = new();

        protected override void Initialize()
        {
            _summonSpellList = Panel.SummonSpellList;
            _playerSpellList = Panel.PlayerSpellList;

            _bars.Add(Panel.HpBar);
            _bars.Add(Panel.EssenceBar);

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

        public void UpdateBar<T>(float newValue, float newMaxValue = 0) where T : BarBase
        {
            var bar = GetBar<T>();

            if (bar == null)
            {
                return;
            }

            bar.Initialize(newMaxValue);
            
            bar.UpdateBar(newValue);
        }

        private BarBase GetBar<T>() where T : BarBase
        {
            for (int i = 0, count = _bars.Count; i < count; i++)
            {
                if (_bars[i].GetType() == typeof(T))
                {
                    return _bars[i];
                }
            }

            return default;
        }
    }
}