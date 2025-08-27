using Itibsoft.PanelManager;
using Project.Scripts.Core.Abstracts;
using Project.Scripts.UI;
using Zenject;

namespace Project.Scripts.Core
{
    public class BookInteractable : InteractableBase
    {
        [Inject] private IPanelManager _panelManager;

        private SpellListOverlayController _spellListOverlayController;

        private void Start()
        {
            _spellListOverlayController = _panelManager.LoadPanel<SpellListOverlayController>();
        }

        public override void Interact()
        {
            _spellListOverlayController.Open();
        }
    }
}