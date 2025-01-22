using Itibsoft.PanelManager;

namespace Project.Scripts.Core
{
    public class NpcBase : DialogableBase
    {
        public NpcTypes NpcType;
        
        public override void Interact()
        {
            CloseButton();
            ShowDialogue();
        }
    }
}