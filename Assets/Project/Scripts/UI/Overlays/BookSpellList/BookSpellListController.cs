using System;
using Itibsoft.PanelManager;
using Project.Scripts.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "BookSpellListView")]
    public class BookSpellListController : PanelControllerBase<BookSpellListView>, IDisposable
    {
        private SpellTip _spellTip;
        private readonly SpellsConfig _spellConfig;
        private readonly ResourcesConfig _resourcesConfig;
        
        public BookSpellListController(SpellTip spellTip, SpellsConfig spellConfig, ResourcesConfig resourcesConfig)
        {
            _spellTip = spellTip;
            _spellConfig = spellConfig;
            _resourcesConfig = resourcesConfig;
        }
        
        protected override void Initialize()
        {
            Panel.CloseButton.onClick.AddListener(Close);
            
            
            var canvas = Object.FindFirstObjectByType<Canvas>();
            _spellTip = Object.Instantiate(_spellTip, canvas.transform);
            
            InitSpellsList();
        }

        private void InitSpellsList()
        {
            foreach (var uiButton in Panel.BloodUIButtons)
            {
                var spellConfig = _spellConfig.GetSpellConfig(uiButton.Spell.GetType());

                uiButton.Init(_spellTip, spellConfig, _resourcesConfig);
            }
            
            foreach (var uiButton in Panel.BonesUIButtons)
            {
                var spellConfig = _spellConfig.GetSpellConfig(uiButton.Spell.GetType());

                uiButton.Init(_spellTip, spellConfig, _resourcesConfig);
            }
            
            foreach (var uiButton in Panel.SoulUIButtons)
            {
                var spellConfig = _spellConfig.GetSpellConfig(uiButton.Spell.GetType());

                uiButton.Init(_spellTip, spellConfig, _resourcesConfig);
            }
            
            foreach (var uiButton in Panel.FleshUIButtons)
            {
                var spellConfig = _spellConfig.GetSpellConfig(uiButton.Spell.GetType());

                uiButton.Init(_spellTip, spellConfig, _resourcesConfig);
            }
        }


        public void Dispose()
        {
            Panel.CloseButton.onClick.RemoveListener(Close);
        }
    }
}