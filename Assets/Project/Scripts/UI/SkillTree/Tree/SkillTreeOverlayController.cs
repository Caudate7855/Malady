using System.Collections.Generic;
using Itibsoft.PanelManager;

namespace Project.Scripts.SkillTree
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "PassiveSkillTreeWindow")]
    public class SkillTreeOverlayController : PanelControllerBase<PassiveSkillTreeView>
    {
        private List<Skill> _skillsList = new();
        
        protected override void Initialize()
        {
            Panel.CreateSkillsList();
            
            _skillsList = Panel.SkillsList;
        }
    }
}