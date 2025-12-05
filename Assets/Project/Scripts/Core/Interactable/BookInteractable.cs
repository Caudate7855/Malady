using Itibsoft.PanelManager;
using Project.Scripts.Abstracts;
using Zenject;

namespace Project.Scripts
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