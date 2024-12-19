namespace Project.Scripts.Core.Interfaces
{
    public interface IInteractable
    {
        public float InteractionCooldownInSeconds { get; set; }
        public void Interact();
    }
}