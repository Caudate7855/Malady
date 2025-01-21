using Project.Scripts.DialogueSystem;

namespace Project.Scripts.Core
{
    public class NpcBase : DialogableBase
    {
        private DialogueWindowController _dialogueWindowController;
        
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }
    }
}