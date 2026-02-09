using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Project.Scripts
{
    public sealed class SpellSlot : DragAndDropSlot, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private SpellParentType _spellParentType;

        [ShowIf("SpellParentType", SpellParentType.Book)]
        [ShowInInspector]
        [SerializeReference]
        [TypeFilter(nameof(GetFilteredTypeList))]
        public SpellBase Spell;

        [ShowIf("@_spellParentType != SpellParentType.Book")]
        [SerializeField] private int _hotbarIndex = -1;

        [Space]
        public RectTransform ItemsContainer;

        [SerializeField] private Image _image;

        private ISpellTipService _tipService;
        private SpellsConfig _spellsConfig;
        private ResourcesConfig _resourcesConfig;
        private SpellConfig _bookSpellConfig;

        public SpellParentType SpellParentType => _spellParentType;
        public int HotbarIndex => _hotbarIndex;

        public new SpellItem Item => (SpellItem)base.Item;
        public bool IsContainItem => base.HasItem;

        public void Init(ISpellTipService tipService, SpellsConfig spellsConfig, ResourcesConfig resourcesConfig, SpellConfig bookSpellConfig = null)
        {
            _tipService = tipService;
            _spellsConfig = spellsConfig;
            _resourcesConfig = resourcesConfig;
            _bookSpellConfig = bookSpellConfig;

            if (_spellParentType == SpellParentType.Book && bookSpellConfig != null)
            {
                Spell = bookSpellConfig.Type;

                if (_image != null)
                {
                    _image.sprite = bookSpellConfig.Icon;
                }
            }
        }

        public SpellItem CreateNewItem(SpellItem itemPrefab, GameObject parentObject)
        {
            var parent = ItemsContainer != null ? ItemsContainer : parentObject.GetComponent<RectTransform>();
            var item = Instantiate(itemPrefab, parent);

            var rt = (RectTransform)item.transform;
            rt.anchoredPosition = Vector2.zero;

            AddItem(item);

            return item;
        }

        public void AddItem(SpellItem item)
        {
            if (item == null)
            {
                return;
            }

            item.CurrentSpellSlot = this;
            base.SetItem(item);

            var rt = (RectTransform)item.transform;
            rt.anchoredPosition = Vector2.zero;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnItemPointerEnter();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnItemPointerExit();
        }

        public void OnItemPointerEnter()
        {
            if (_tipService == null || _spellsConfig == null || _resourcesConfig == null)
            {
                return;
            }

            var cfg = ResolveSpellConfig();
            if (cfg == null)
            {
                return;
            }

            _tipService.Show(cfg, _resourcesConfig);
        }

        public void OnItemPointerExit()
        {
            if (_tipService == null)
            {
                return;
            }

            _tipService.Hide();
        }

        private SpellConfig ResolveSpellConfig()
        {
            if (_spellParentType == SpellParentType.Book)
            {
                return _bookSpellConfig;
            }

            var item = Item;
            if (item == null)
            {
                return null;
            }

            if (item.SpellConfig != null)
            {
                return item.SpellConfig;
            }

            if (item.Spell == null)
            {
                return null;
            }

            return _spellsConfig.GetSpellConfig(item.Spell.GetType());
        }

        private IEnumerable<Type> GetFilteredTypeList()
        {
            return typeof(SpellBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(SpellBase).IsAssignableFrom(x));
        }
    }
}
