using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Overlays;
using Zenject;

namespace Project.Scripts.SpellCaster
{
    public class SpellCaster
    {
        [Inject] private PlayerFsm _playerFsm;
        [Inject] private IPanelManager _panelManager;

        private MainUIController _mainUIController;
        private List<SpellSo> _playerSpells = new(4);

        public SpellCaster()
        {
            _mainUIController = _panelManager.LoadPanel<MainUIController>();
        }

        public void AddPlayerSpell(int index, SpellSo spell)
        {
            _playerSpells[index] = spell;
        }

        public void RemovePlayerSpell(int index)
        {
            _playerSpells.Remove(_playerSpells[index]);
        }

        public SpellSo GetPlayerSpell(int index)
        {
            return _playerSpells[index];
        }
        
        public void CastSpell(SpellSo spell)
        {
            _playerFsm.SetState<PlayerFsmStateCast>();
        }
    }
}