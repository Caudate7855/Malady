using Itibsoft.PanelManager;
using Project.Scripts.Configs;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "BookSpellListView")]
    public class BookSpellListController : PanelControllerBase<BookSpellListView>
    {
        private readonly SpellTip _spellTip;
        private readonly SpellsConfig _spellConfig;
        private readonly ResourcesConfig _resourcesConfig;
        
        public BookSpellListController(SpellTip spellTip, SpellsConfig spellConfig, ResourcesConfig resourcesConfig)
        {
            _spellTip = spellTip;
            _spellConfig = spellConfig;
            _resourcesConfig = resourcesConfig;
        }
        
        protected override void Initialize()
        {
            InitSpellsList();
        }

        private void InitSpellsList()
        {
            foreach (var spellConfig in _spellConfig.SpellConfigs)
            {
                if (spellConfig.ElementType == SpellElementType.Blood)
                {
                    
                }
            }
        }
    }
}