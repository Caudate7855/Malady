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
    [RequireComponent(typeof(Image))]
    [RequireComponent(typeof(Button))]
    public class SpellUIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler,
        IEndDragHandler, IDragHandler
    {
        public SpellParentType SpellParentType;
        
        [ShowIf("SpellParentType", SpellParentType.Book)]
        [ShowInInspector]
        [TypeFilter(nameof(GetFilteredTypeList))]
        public SpellBase Spell;
        
        private SpellTip _spellTip;
        private SpellConfig _spellConfig;
        private ResourceConfig _resourceConfig;
        
        private void Awake()
        {
            if (SpellParentType == SpellParentType.Book)
            {
                
            }
        }

        public void Init(SpellTip spellTip, SpellConfig spellConfig, ResourceConfig resourceConfig)
        {
            _spellTip = spellTip;
            _spellConfig = spellConfig;
            _resourceConfig = resourceConfig;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _spellTip.SetInfo(_spellConfig, _resourceConfig);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
        
        private IEnumerable<Type> GetFilteredTypeList()
        {
            var type = typeof(SpellBase).Assembly.GetTypes()
                .Where(x => !x.IsAbstract)
                .Where(x => typeof(SpellBase).IsAssignableFrom(x));

            return type;
        }
    }
}