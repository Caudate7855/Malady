using Itibsoft.PanelManager;
using Project.Scripts.Abstracts;
using Zenject;

namespace Project.Scripts
{
    public class BookInteractable : InteractableBase
    {
        [Inject] private IPanelManager _panelManager;

        private BookSpellListController _bookSpellListController;

        private void Start()
        {
            _bookSpellListController = _panelManager.LoadPanel<BookSpellListController>();
        }

        public override void Interact()
        {
            _bookSpellListController.Open();
        }
    }
}