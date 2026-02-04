using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class InventoryView : PanelBase
    {
        [SerializeField] private Button _statsButton;
        [SerializeField] private List<InventorySlot> _inventorySlots;
        [SerializeField] private RectTransform _statsWindowRectTransform;
        [SerializeField] private GameObject _itemsContainer;
        [SerializeField] private RectTransform _statsContainer;

        public Button StatsButton => _statsButton;
        public List<InventorySlot> InventorySlots => _inventorySlots;
        public RectTransform StatsWindowRectTransform => _statsWindowRectTransform;
        public RectTransform StatsContainer => _statsContainer;
        public RectTransform ItemsContainer => _itemsContainer.GetComponent<RectTransform>();
    }
}