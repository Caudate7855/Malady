using System.Collections.Generic;
using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Overlays.Inventory
{
    public class InventoryView : PanelBase
    {
        [SerializeField] private Button _statsButton;
        [SerializeField] private List<InventorySlot> _inventorySlots;
        [SerializeField] private List<TMP_Text> _statsViewList; 
        [SerializeField] private RectTransform _statsWindowRectTransform;
        [SerializeField] private GameObject _itemsContainer;

        public Button StatsButton => _statsButton;
        public List<InventorySlot> InventorySlots => _inventorySlots;
        public List<TMP_Text> StatsViewList => _statsViewList;
        public RectTransform StatsWindowRectTransform => _statsWindowRectTransform;
        public RectTransform ItemsContainer => _itemsContainer.GetComponent<RectTransform>();
    }
}