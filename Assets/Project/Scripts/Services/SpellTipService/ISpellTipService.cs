using Project.Scripts.Configs;

namespace Project.Scripts
{
    public interface ISpellTipService
    {
        void BindCanvas();
        void Show(SpellConfig spellConfig, ResourcesConfig resourcesConfig);
        void Hide();
    }
}