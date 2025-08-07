using Itibsoft.PanelManager;
using JetBrains.Annotations;
using Project.Scripts.Overlays;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SpellSystemController
    {
        [Inject] private IPanelManager _panelManager;
        [Inject] private PlayerFsm _playerFsm;
        [Inject] private SummonSystem _summonSystem;
        [Inject] private SpellsLogicsList _spellsLogicsList;

        public SpellList SummonedSpellList { get; private set; }
        public SpellList PlayerSpellList { get; private set; }

        public void Initialize()
        {
            var mainUIController = _panelManager.LoadPanel<MainUIController>();

            SummonedSpellList = mainUIController.SummonSpellList;
            PlayerSpellList = mainUIController.PlayerSpellList;
        }

        public void CastSummonSpellByIndex(int indexToCast)
        {
            //SummonedSpellList.ChosenSpells[indexToCast].Cast();
        }
        
        public void CastPlayerSpellByIndex(int indexToCast)
        {
            _spellsLogicsList.CastSpell(PlayerSpellList.ChosenSpells[indexToCast].Id);
        }
    }
}