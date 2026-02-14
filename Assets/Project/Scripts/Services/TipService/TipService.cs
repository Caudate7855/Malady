using System;
using Itibsoft.PanelManager;
using Project.Scripts.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    public sealed class TipService : ITipService, IDisposable
    {
        private readonly IPanelManager _panelManager;

        private readonly SpellTip _spellTipPrefab;
        private readonly ItemTip _itemTipPrefab;

        private SpellTip _spellTipInstance;
        private ItemTip _itemTipInstance;

        private Canvas _canvas;

        public TipService(IPanelManager panelManager, SpellTip spellTipPrefab, ItemTip itemTipPrefab)
        {
            _panelManager = panelManager;
            _spellTipPrefab = spellTipPrefab;
            _itemTipPrefab = itemTipPrefab;
        }

        public void BindCanvas()
        {
            _canvas = _panelManager.PanelDispatcher.Canvas;
            if (_canvas == null)
                throw new Exception("TipService: canvas is null");

            if (_spellTipInstance == null)
                CreateSpellTip();

            if (_itemTipInstance == null)
                CreateItemTip();
        }

        public void ShowSpellTip(SpellConfig spellConfig, ResourcesConfig resourcesConfig)
        {
            if (spellConfig == null || resourcesConfig == null)
                return;

            BindCanvas();

            if (_spellTipInstance == null)
                return;

            if (_itemTipInstance != null)
                _itemTipInstance.Close();

            _spellTipInstance.SetInfo(spellConfig, resourcesConfig);
            _spellTipInstance.Open();
        }

        public void ShowItemTip(ItemData itemData)
        {
            if (itemData == null)
                return;

            BindCanvas();

            if (_itemTipInstance == null)
                return;

            if (_spellTipInstance != null)
                _spellTipInstance.Close();

            _itemTipInstance.SetInfo(itemData);
            _itemTipInstance.Open();
        }

        public void Hide()
        {
            if (_spellTipInstance != null)
                _spellTipInstance.Close();

            if (_itemTipInstance != null)
                _itemTipInstance.Close();
        }

        public void Dispose()
        {
            if (_spellTipInstance != null)
            {
                Object.Destroy(_spellTipInstance.gameObject);
                _spellTipInstance = null;
            }

            if (_itemTipInstance != null)
            {
                Object.Destroy(_itemTipInstance.gameObject);
                _itemTipInstance = null;
            }
        }

        private void CreateSpellTip()
        {
            if (_spellTipPrefab == null)
                throw new Exception("TipService: spell tip prefab is null");

            _spellTipInstance = Object.Instantiate(_spellTipPrefab, _canvas.transform);
            _spellTipInstance.Init(_canvas);
            _spellTipInstance.Close();
        }

        private void CreateItemTip()
        {
            if (_itemTipPrefab == null)
                throw new Exception("TipService: item tip prefab is null");

            _itemTipInstance = Object.Instantiate(_itemTipPrefab, _canvas.transform);
            _itemTipInstance.Init(_canvas);
            _itemTipInstance.Close();
        }
    }
}
