using System.Collections.Generic;
using Itibsoft.PanelManager;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "SpellListOverlay")]
    public class SpellListOverlayController : PanelControllerBase<SpellListOverlay>
    {
        [Inject] private IPanelManager _panelManager;

        private SkillTreeOverlayController _skillTreeOverlayController;


        protected override void OnOpenBefore()
        {
           
        }

        private void OnPassivePerksButtonPressed()
        {
            _skillTreeOverlayController.Open();
            Close();
        }
        
        private void OnMemoriesButtonPressed()
        {
            
        }
        
        private void SetSpells()
        {
            
        }
    }
}