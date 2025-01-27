using System.Collections.Generic;
using Itibsoft.PanelManager;

namespace Project.Scripts.Overlays
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "SpellListOverlay")]
    public class SpellListOverlayController : PanelControllerBase<SpellListOverlay>
    {
        private List<SpellButtonBase> _bloodSpellsList;
        private List<SpellButtonBase> _soulsSpellsList;
        private List<SpellButtonBase> _bonesSpellsList;
        private List<SpellButtonBase> _fleshSpellsList;
        
        protected override void Initialize()
        {
            
        }
    }
}