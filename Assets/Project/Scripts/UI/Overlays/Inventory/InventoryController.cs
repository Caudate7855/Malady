using System.Collections.Generic;
using DG.Tweening;
using Itibsoft.PanelManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.Overlays.Inventory
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "InventoryView")]
    public class InventoryController : PanelControllerBase<InventoryView>
    {
        [Inject] private IStatSystem _statSystem;
        [Inject] private InventoryItem _baseItem;

        private Button _statsWindowButton;
        private List<TMP_Text> _statsViewList;
        private bool _isStatsWindowOpened;
        
        private RectTransform _statsWindowRectTransform;
        private RectTransform _defaultStatsWindowRectTransform;
        
        private List<InventorySlot> _inventorySlots;
        private RectTransform _itemsContainer;

        protected override void Initialize()
        {
            _statsWindowButton = Panel.StatsButton;
            _statsViewList = Panel.StatsViewList;
            _inventorySlots = Panel.InventorySlots;
            _itemsContainer = Panel.ItemsContainer;
            
            InitializeTestItems();
            
            _statsWindowButton.onClick.AddListener(OnStatsButtonClicked);
            _statsWindowRectTransform = Panel.StatsWindowRectTransform;

            _defaultStatsWindowRectTransform = _statsWindowRectTransform;
            
            InitializeStatsView();
        }

        private void InitializeTestItems()
        {
            _inventorySlots[0].CreateNewItem(_baseItem, _itemsContainer.gameObject);
            _inventorySlots[1].CreateNewItem(_baseItem, _itemsContainer.gameObject);
            _inventorySlots[2].CreateNewItem(_baseItem, _itemsContainer.gameObject);
        }

        private void InitializeStatsView()
        {
            var stats = _statSystem.GetStats();

            for (int i = 0, count = stats.Count; i < count; i++)
            {
                _statsViewList[i].text = stats[i].Value.ToString();
            }
            
            CloseStatsWindow();
        }

        private void OnStatsButtonClicked()
        {
            _isStatsWindowOpened = !_isStatsWindowOpened;

            if (_isStatsWindowOpened)
            {
                OpenStatsWindow();
            }
            else
            {
                CloseStatsWindow();
            }
        }

        private void OpenStatsWindow()
        {
            _statsWindowRectTransform.DOAnchorPosX(0f,0.5f);
        }

        private void CloseStatsWindow()
        {
            _statsWindowRectTransform.DOAnchorPosX(300f,0.5f);
        }
    }
}