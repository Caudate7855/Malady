using Project.Scripts.DialogueSystem;

namespace Project.Scripts.Core
{
    public class NpcBase : DialogableBase
    {
        public NpcTypes NpcType;
        private DialogueWindowController _dialogueWindowController;
        
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }

        protected override void InitializeDialogueSystem()
        {
            
        }
    }
}