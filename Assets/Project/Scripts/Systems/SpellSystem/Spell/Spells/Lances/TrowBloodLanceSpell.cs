using System.Collections.Generic;
using JetBrains.Annotations;
using Project.Scripts.UI.Inventory;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class TrowBloodLanceSpell : SpellBase
    {
        private BloodLanceBonusDamageStat _bloodLanceBonusDamageStat;
        [Inject] private PlayerSpellModificatorsSystem _playerSpellModificatorsSystem;
        
        private List<SpellModificatorBase> _spellModificators = new();
        
        public override void Initialize()
        {
            ID = "blood_0_1";
            IsInitialized = true;
            
            _bloodLanceBonusDamageStat = PlayerStats.GetStat<BloodLanceBonusDamageStat>();
            InventoryController = PanelManager.LoadPanel<InventoryController>();
            Type = _bloodLanceBonusDamageStat.Type;
            
            _playerSpellModificatorsSystem.AddModificator(new BloodLanceModificatorArea());
            
            _spellModificators.Add(_playerSpellModificatorsSystem.GetModificatorByID("1"));
        }

        public override void Cast()
        {
            if (_spellModificators.Contains(_playerSpellModificatorsSystem
                    .GetModificatorByType<BloodLanceModificatorArea>()))
            {
                Debug.Log("MODIF");
            }
            else
            {
                Debug.Log("No_MODIF");
            }
        }

        public override void Clear()
        {
            
        }
    }
}