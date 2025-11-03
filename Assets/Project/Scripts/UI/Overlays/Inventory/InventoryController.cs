using System.Collections.Generic;
using DG.Tweening;
using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.UI.Inventory
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "InventoryView")]
    public class InventoryController : PanelControllerBase<InventoryView>
    {
        [Inject] private PlayerStats _statSystem;
        [Inject] private InventoryItem _baseItem;
        [Inject] private StatView _statView;

        private Button _statsWindowButton;
        private StatsListView _statsListView;
        private bool _isStatsWindowOpened;
        
        private RectTransform _statsWindowRectTransform;
        private RectTransform _defaultStatsWindowRectTransform;
        
        private List<InventorySlot> _inventorySlots;
        private RectTransform _itemsContainer;
        private RectTransform _statsContainer;

        protected override void Initialize()
        {
            _statsWindowButton = Panel.StatsButton;
            _statsListView = Panel.StatsListView;
            _inventorySlots = Panel.InventorySlots;
            _itemsContainer = Panel.ItemsContainer;
            _statsContainer = Panel.StatsContainer;
            
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
            var stats = _statSystem.GetAllStats();

            for (int i = 0; i < stats.Count; i++)
            {
                var statView = Object.Instantiate(_statView, _statsContainer);
                statView.NameText.text = stats[i].Name;
                statView.ValueText.text = stats[i].Value.ToString();
                statView.Type =  stats[i].Type;
                
                var rectTransform = statView.GetComponent<RectTransform>();
                var size = rectTransform.sizeDelta;
                size.x = 280f;
                rectTransform.sizeDelta = size;
                
                _statsListView.StatViews.Add(statView);
            }
            
            CloseStatsWindow();
        }

        public void UpdateStatView(StatType type, float newValue)
        {
            for (int i = 0; i <  _statsListView.StatViews.Count; i++)
            {
                if (_statsListView.StatViews[i].Type == type)
                {
                    _statsListView.StatViews[i].ValueText.text = newValue.ToString();
                }
            }
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