namespace Project.Scripts
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