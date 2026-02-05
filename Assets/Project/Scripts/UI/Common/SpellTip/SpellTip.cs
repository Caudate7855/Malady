using System.Collections.Generic;
using Project.Scripts.Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class SpellTip : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        
        [SerializeField] private RectTransform _resourceObjectContainer;
        [SerializeField] private SpellTipResource _spellTipResourcePrefab;

        private readonly List<SpellTipResource> _spellTipResources = new();

        public void SetInfo(SpellConfig spellConfig, ResourcesConfig resourceConfig)
        {
            _image.sprite = spellConfig.Icon;
            _name.text = spellConfig.Name;
            _description.text = spellConfig.Description;

            FillResourceObjects(spellConfig.Cost, resourceConfig);
        }

        private void FillResourceObjects(Dictionary<ResourceType, float> cost, ResourcesConfig resourceConfig)
        {
            var used = 0;

            foreach (var kv in cost)
            {
                SpellTipResource view;

                if (used < _spellTipResources.Count)
                {
                    view = _spellTipResources[used];
                    view.gameObject.SetActive(true);
                }
                else
                {
                    view = Instantiate(_spellTipResourcePrefab, _resourceObjectContainer);
                    _spellTipResources.Add(view);
                }

                var sprite = resourceConfig.GetResourceConfig(kv.Key).Icon; 
                
                view.SetInfo(sprite, kv.Value);
                used++;
            }

            for (var i = used; i < _spellTipResources.Count; i++)
            {
                _spellTipResources[i].gameObject.SetActive(false);
            }
        }
    }
}