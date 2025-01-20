using Project.Scripts.Core.Abstracts;

namespace Project.Scripts.Core
{
    public class Blacksmith : InteractableZoneBase
    {
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }

        private void ShowDialogue()
        {
            PanelManager.LoadPanel<DialogueWindowController>();
        }
    }
}