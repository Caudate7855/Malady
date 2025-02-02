using Itibsoft.PanelManager;
using JetBrains.Annotations;
using Project.Scripts.Overlays;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SpellTipHandler
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private SpellTip _spellTip;

        private bool _isCreated;

        public SpellTipHandler(SpellTip spellTip)
        {
            _spellTip = spellTip;
        }

        public SpellTip GetSpellTip()
        {
            if (_isCreated == false)
            {
                _spellTip = _diContainer.InstantiatePrefabForComponent<SpellTip>(_spellTip,
                    Object.FindObjectOfType<PanelDispatcher>().GetComponent<Canvas>().transform);
                
                _isCreated = true;
            }

            return _spellTip;
        }
    }
}