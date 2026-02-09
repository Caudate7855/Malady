using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "BookSpellListView")]
    public class BookSpellListController : PanelControllerBase<BookSpellListView>, IDisposable
    {
        [SerializeField] private bool _debug = true;

        private SpellTip _spellTip;

        private readonly SpellsConfig _spellConfig;
        private readonly ResourcesConfig _resourcesConfig;
        private readonly IPanelManager _panelManager;
        private readonly SpellItem _spellItemPrefab;

        public BookSpellListController(
            SpellTip spellTip,
            SpellsConfig spellConfig,
            ResourcesConfig resourcesConfig,
            IPanelManager panelManager,
            SpellItem spellItemPrefab)
        {
            _spellTip = spellTip;
            _spellConfig = spellConfig;
            _resourcesConfig = resourcesConfig;
            _panelManager = panelManager;
            _spellItemPrefab = spellItemPrefab;
        }

        protected override void Initialize()
        {
            Panel.CloseButton.onClick.AddListener(Close);

            _spellTip = Object.Instantiate(_spellTip, _panelManager.PanelDispatcher.Canvas.transform);
            _spellTip.Init(_panelManager.PanelDispatcher.Canvas);

            if (_debug)
            {
                Debug.Log("[BookSpellList] Initialize");
                Debug.Log($"[BookSpellList] Prefab={( _spellItemPrefab != null ? _spellItemPrefab.name : "NULL")}");
            }

            InitSpellsList();
        }

        private void InitSpellsList()
        {
            var all = new List<SpellSlot>();
            Panel.GetComponentsInChildren(true, all);

            if (_debug)
            {
                Debug.Log($"[BookSpellList] AutoScan SpellSlots count={all.Count}");
            }

            for (var i = 0; i < all.Count; i++)
            {
                InitSlot("Auto", i, all[i]);
            }
        }

        private void InitGroup(string name, List<SpellSlot> slots)
        {
            if (slots == null)
            {
                if (_debug) Debug.Log($"[BookSpellList] Group {name} slots=NULL");
                return;
            }

            if (_debug) Debug.Log($"[BookSpellList] Group {name} count={slots.Count}");

            for (var i = 0; i < slots.Count; i++)
            {
                InitSlot(name, i, slots[i]);
            }
        }

        private void InitSlot(string group, int index, SpellSlot slot)
        {
            if (slot == null)
            {
                if (_debug) Debug.Log($"[BookSpellList] {group}[{index}] slot=NULL");
                return;
            }

            if (_debug)
            {
                Debug.Log($"[BookSpellList] {group}[{index}] slot={slot.name} parentType={slot.SpellParentType} hasItem={slot.IsContainItem}");
            }

            if (slot.Spell == null)
            {
                if (_debug) Debug.Log($"[BookSpellList] {group}[{index}] slot={slot.name} Spell=NULL");
                return;
            }

            SpellConfig spellConfig;
            try
            {
                spellConfig = _spellConfig.GetSpellConfig(slot.Spell.GetType());
            }
            catch (Exception e)
            {
                Debug.LogError($"[BookSpellList] {group}[{index}] slot={slot.name} GetSpellConfig failed: {e.Message}");
                return;
            }

            slot.Init(_spellTip, spellConfig, _resourcesConfig);

            if (slot.IsContainItem)
            {
                if (_debug) Debug.Log($"[BookSpellList] {group}[{index}] slot={slot.name} already has item after Init()");
                return;
            }

            if (_spellItemPrefab == null)
            {
                Debug.LogError("[BookSpellList] SpellItem prefab is NULL");
                return;
            }

            var parent = slot.ItemsContainer != null ? slot.ItemsContainer : (RectTransform)slot.transform;

            var item = Object.Instantiate(_spellItemPrefab, parent);
            item.gameObject.SetActive(true);

            item.Spell = spellConfig.Type;
            item.SetIcon(spellConfig.Icon);

            slot.AddItem(item);

            var rt = (RectTransform)item.transform;
            rt.anchoredPosition = Vector2.zero;

            if (_debug)
            {
                var p = item.transform.parent != null ? item.transform.parent.name : "NULL";
                Debug.Log($"[BookSpellList] {group}[{index}] CREATED item={item.name} parent={p} slotNowHasItem={slot.IsContainItem}");
            }
        }

        public void Dispose()
        {
            Panel.CloseButton.onClick.RemoveListener(Close);
        }
    }
}
