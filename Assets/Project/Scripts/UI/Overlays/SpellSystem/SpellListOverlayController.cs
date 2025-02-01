using System.Collections.Generic;
using Itibsoft.PanelManager;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "SpellListOverlay")]
    public class SpellListOverlayController : PanelControllerBase<SpellListOverlay>
    {
        private List<SpellUIButtonBase> _bloodSpellsList;
        private List<SpellUIButtonBase> _soulsSpellsList;
        private List<SpellUIButtonBase> _bonesSpellsList;
        private List<SpellUIButtonBase> _fleshSpellsList;
        
        protected override void Initialize()
        {
            
        }
    }
}