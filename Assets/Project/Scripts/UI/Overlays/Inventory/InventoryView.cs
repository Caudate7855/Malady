using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;

namespace Project.Scripts.UI.Overlays.Inventory
{
    public class InventoryView : PanelBase
    {
        [SerializeField] private List<InventorySlot> _inventorySlots;
    }
}