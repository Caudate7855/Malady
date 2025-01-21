using Itibsoft.PanelManager;
using Project.Scripts.DialogueSystem;

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