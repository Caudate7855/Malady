namespace Project.Scripts.UI
{
    public interface IBar
    {
        public void Initialize(float maxValue);
        public void UpdateBar(float newValue);
    }
}