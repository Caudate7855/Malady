using Itibsoft.PanelManager;
using Project.Scripts.Abstracts;
using Zenject;

namespace Project.Scripts
{
    public class BookInteractable : InteractableBase
    {
        [Inject] private IPanelManager _panelManager;

        private void Start()
        {
            
        }

        public override void Interact()
        {
            
        }
    }
}