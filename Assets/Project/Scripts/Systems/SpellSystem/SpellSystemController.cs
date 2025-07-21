using System.Collections.Generic;
using Itibsoft.PanelManager;
using JetBrains.Annotations;
using Project.Scripts.Overlays;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SpellSystemController
    {
        [Inject] private IPanelManager _panelManager;
        [Inject] private PlayerFsm _playerFsm;

        private MainUIController _mainUIController;
        
        private SpellList _summonedSpellList;
        private SpellList _playerSpellList;

        public SpellSystemController()
        {
            _mainUIController = _panelManager.LoadPanel<MainUIController>();

            _summonedSpellList = _mainUIController.SummonSpellList;
            _playerSpellList = _mainUIController.PlayerSpellList;
        }
        
        public void CastSpell(SpellSo spell)
        {
            
        }
    }
}