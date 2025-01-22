using Itibsoft.PanelManager;
using Project.Scripts.Core.Abstracts;
using Zenject;

namespace Project.Scripts
{
    public abstract class DialogableBase : InteractableZoneBase , IDialogable
    {
        [Inject] private IPanelManager PanelManager;
        [Inject] private DialogueSystemManager _dialogueSystem;
        
        private DialogueWindowController _dialogueWindowController;

        
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }

        public void ShowDialogue()
        {
            _dialogueWindowController = PanelManager.LoadPanel<DialogueWindowController>();
            _dialogueWindowController.Open();
        }
    }
}