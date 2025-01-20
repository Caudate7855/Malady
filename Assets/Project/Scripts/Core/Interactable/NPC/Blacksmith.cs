using Itibsoft.PanelManager;
using Project.Scripts.Core.Abstracts;

namespace Project.Scripts.Core
{
    public class Blacksmith : InteractableZoneBase
    {
        public IPanelManager PanelManager;
        private DialogueWindowController _dialogueWindowController;
        
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }

        private void ShowDialogue()
        {
            _dialogueWindowController = PanelManager.LoadPanel<DialogueWindowController>();
            _dialogueWindowController.Open();
        }
    }
}