using Itibsoft.PanelManager;

namespace Project.Scripts.Core
{
    public class NpcBase : DialogableBase
    {
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }
    }
}