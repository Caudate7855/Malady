using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI.Inventory
{
    public class InventoryView : PanelBase
    {
        [SerializeField] private Button _statsButton;
        [SerializeField] private List<InventorySlot> _inventorySlots;
        [SerializeField] private StatsListView _statsListView; 
        [SerializeField] private RectTransform _statsWindowRectTransform;
        [SerializeField] private GameObject _itemsContainer;
        [SerializeField] private RectTransform _statsContainer;

        public Button StatsButton => _statsButton;
        public List<InventorySlot> InventorySlots => _inventorySlots;
        public StatsListView StatsListView => _statsListView;
        public RectTransform StatsWindowRectTransform => _statsWindowRectTransform;
        public RectTransform StatsContainer => _statsContainer;
        public RectTransform ItemsContainer => _itemsContainer.GetComponent<RectTransform>();
    }
}