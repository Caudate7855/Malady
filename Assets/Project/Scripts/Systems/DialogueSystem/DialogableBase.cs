using Itibsoft.PanelManager;
using Project.Scripts.Abstracts;
using Zenject;

namespace Project.Scripts
{
    public abstract class DialogableBase : InteractableZoneBase
    {
        public NpcTypes NpcType;
        
        [Inject] private IPanelManager PanelManager;
        private DialogueWindowController _dialogueWindowController;
        
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }

        public void ShowDialogue()
        {
            _dialogueWindowController = PanelManager.LoadPanel<DialogueWindowController>();
            _dialogueWindowController.CurrentNpcType = NpcType;
            
            _dialogueWindowController.Open();
            _dialogueWindowController.ShowDialogueWindow();
        }
    }
}