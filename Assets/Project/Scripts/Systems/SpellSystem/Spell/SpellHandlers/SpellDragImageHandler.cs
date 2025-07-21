using Itibsoft.PanelManager;
using JetBrains.Annotations;
using Project.Scripts.Overlays;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SpellDragImageHandler
    {
        [Inject] private SpellDragImage _spellDragImage;
        [Inject] private DiContainer _diContainer;

        private bool _isCreated;
        
        public SpellDragImage GetSpellDragImage()
        {
            if (_isCreated == false)
            {
                _spellDragImage = _diContainer.InstantiatePrefabForComponent<SpellDragImage>(_spellDragImage,
                    Object.FindObjectOfType<PanelDispatcher>().GetComponent<Canvas>().transform);
                
                _isCreated = true;
            }

            return _spellDragImage;
        }
    }
}