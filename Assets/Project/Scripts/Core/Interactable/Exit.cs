using Project.Scripts.Core.Abstracts;

namespace Project.Scripts.Core
{
    public class Exit : InteractableZoneBase
    {
        public override void Interact()
        {
            CloseButton();
        }
    }
}