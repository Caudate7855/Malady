using System;
using Itibsoft.PanelManager;
using Project.Scripts.Configs;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    public sealed class SpellTipService : ISpellTipService, IDisposable
    {
        private readonly IPanelManager _panelManager;
        private readonly SpellTip _spellTipPrefab;

        private SpellTip _tipInstance;
        private Canvas _canvas;

        public SpellTipService(IPanelManager panelManager, SpellTip spellTipPrefab)
        {
            _panelManager = panelManager;
            _spellTipPrefab = spellTipPrefab;
        }

        public void BindCanvas()
        {
            _canvas = _panelManager.PanelDispatcher.Canvas;
            if (_canvas == null)
            {
                throw new Exception("SpellTipService: canvas is null");
            }

            if (_tipInstance != null)
            {
                return;
            }

            if (_spellTipPrefab == null)
            {
                throw new Exception("SpellTipService: spell tip prefab is null");
            }

            _tipInstance = Object.Instantiate(_spellTipPrefab, _canvas.transform);
            _tipInstance.Init(_canvas);
            _tipInstance.Close();
        }

        public void Show(SpellConfig spellConfig, ResourcesConfig resourcesConfig)
        {
            if (spellConfig == null || resourcesConfig == null)
            {
                return;
            }

            BindCanvas();

            if (_tipInstance == null)
            {
                return;
            }

            _tipInstance.SetInfo(spellConfig, resourcesConfig);
            _tipInstance.Open();
        }

        public void Hide()
        {
            if (_tipInstance == null)
            {
                return;
            }

            _tipInstance.Close();
        }

        public void Dispose()
        {
            if (_tipInstance != null)
            {
                Object.Destroy(_tipInstance.gameObject);
                _tipInstance = null;
            }
        }
    }
}