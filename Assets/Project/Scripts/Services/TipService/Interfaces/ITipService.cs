using Project.Scripts.Configs;

namespace Project.Scripts
{
    public interface ITipService
    {
        void BindCanvas();

        void ShowSpellTip(SpellConfig spellConfig, ResourcesConfig resourcesConfig);
        void ShowItemTip(ItemData itemData);

        void Hide();
    }
}