using Itibsoft.PanelManager;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class SpellDragImageHandler
    {
        [Inject] private SpellDragImage _spellDragImage;
        [Inject] private DiContainer _diContainer;
        [Inject] private IPanelManager _panelManager;

        private bool _isCreated;
        
        public SpellDragImage GetSpellDragImage()
        {
            if (_isCreated == false)
            {
                _spellDragImage = _diContainer.InstantiatePrefabForComponent<SpellDragImage>(_spellDragImage,
                    _panelManager.PanelDispatcher.Canvas.transform);
                
                _isCreated = true;
            }

            return _spellDragImage;
        }
    }
}