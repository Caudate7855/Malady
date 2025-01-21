using Itibsoft.PanelManager;
using Project.Scripts.Core.Abstracts;
using Zenject;

namespace Project.Scripts.DialogueSystem
{
    public abstract class DialogableBase : InteractableZoneBase , IDialogable
    {
        [Inject] public IPanelManager PanelManager;
        
        private DialogueWindowController _dialogueWindowController;
        private DialogueSystem _dialogueSystem;
        
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