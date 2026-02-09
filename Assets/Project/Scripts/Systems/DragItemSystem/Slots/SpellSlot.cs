using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Configs;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    public sealed class SpellSlot : DragAndDropSlot
    {
        [SerializeField] private SpellParentType _spellParentType;

        [ShowIf("SpellParentType", SpellParentType.Book)]
        [ShowInInspector]
        [SerializeReference]
        [TypeFilter(nameof(GetFilteredTypeList))]
        public SpellBase Spell;

        [Space]
        public RectTransform ItemsContainer;

        [SerializeField] private Image _image;
        [SerializeField] private SpellItem _bookItemPrefab;

        private SpellTip _spellTip;
        private SpellConfig _spellConfig;
        private ResourcesConfig _resourceConfig;

        public SpellParentType SpellParentType => _spellParentType;

        public new SpellItem Item => (SpellItem)base.Item;
        public bool IsContainItem => base.HasItem;

        public void Init(SpellTip spellTip, SpellConfig spellConfig, ResourcesConfig resourceConfig)
        {
            _spellTip = spellTip;
            _spellConfig = spellConfig;
            _resourceConfig = resourceConfig;

            if (_image != null)
            {
                _image.sprite = spellConfig.Icon;
            }

            if (_spellParentType == SpellParentType.Book)
            {
                Spell = spellConfig.Type;
                EnsureBookItemRuntime();
            }
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

        public void RemoveItemToContainer()
        {
            if (!HasItem)
            {
                return;
            }

            var item = (SpellItem)ClearItem();

            if (ItemsContainer != null)
            {
                var rt = (RectTransform)item.transform;
                rt.SetParent(ItemsContainer, false);
                rt.anchoredPosition = Vector2.zero;
            }

            item.CurrentSpellSlot = null;
        }

        private void EnsureBookItemRuntime()
        {
            if (_spellParentType != SpellParentType.Book)
            {
                return;
            }

            if (Spell == null)
            {
                return;
            }

            if (_bookItemPrefab == null)
            {
                throw new Exception("SpellSlot(Book): _bookItemPrefab is null");
            }

            var item = Item;
            if (item == null)
            {
                var parent = ItemsContainer != null ? ItemsContainer : (RectTransform)transform;
                item = Object.Instantiate(_bookItemPrefab, parent);
            }

            item.Spell = Spell;

            if (_spellConfig != null)
            {
                item.SetIcon(_spellConfig.Icon);

                if (_image != null)
                {
                    _image.sprite = _spellConfig.Icon;
                }
            }

            AddItem(item);
        }

        private IEnumerable<Type> GetFilteredTypeList()
        {
            return typeof(SpellBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(SpellBase).IsAssignableFrom(x));
        }
    }
}
