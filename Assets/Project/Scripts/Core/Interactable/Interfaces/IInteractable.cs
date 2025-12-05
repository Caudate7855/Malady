namespace Project.Scripts.Interfaces
{
    public interface IInteractable
    {
        public float InteractionCooldownInSeconds { get; set; }
        public void Interact();
    }
}