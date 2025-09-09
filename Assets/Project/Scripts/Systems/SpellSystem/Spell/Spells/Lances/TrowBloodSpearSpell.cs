using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.UI.Inventory;
using UnityEngine;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class TrowBloodSpearSpell : ProjectileSpellBase
    {
        private BloodSpearBonusDamageStat _bloodSpearBonusDamageStat;
        
        public override void Initialize()
        {
            PlayerController = Object.FindFirstObjectByType<PlayerController>();
            _bloodSpearBonusDamageStat = PlayerStats.GetStat<BloodSpearBonusDamageStat>();
            InventoryController = PanelManager.LoadPanel<InventoryController>();
            Type = _bloodSpearBonusDamageStat.Type;
            
            ID = "blood_0_1";
            IsInitialized = true;

            //todo: переместить место добавления модификаторов при эквипе шмота / прокачке passive skills / прокачке memories
            PlayerSpellModificatorsSystem.AddModificator(new BloodSpearModificatorArea());
        }

        public override void Cast()
        {
            if (PlayerSpellModificatorsSystem.GetModificatorByType<BloodSpearModificatorArea>() != null)
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