using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Itibsoft.PanelManager;
using UnityEngine;
using Zenject;

namespace Project.Scripts.SkillTree
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "PassiveSkillTreeWindow")]
    public class SkillTreeOverlayController : PanelControllerBase<PassiveSkillTreeView>
    {
        [Inject] private GlobalFactory _globalFactory;
        
        private List<Skill> _skillsList = new();
        private GameObject _edgesParent;        
        
        protected async override void Initialize()
        {
            _skillsList = Panel.GetSkillsList();
            _edgesParent = Panel.ParentObject;

            await CreateSkillsEdges(_skillsList);
        }

        private async UniTask CreateSkillsEdges(List<Skill> skillsList)
        {
            for (int i = 0; i < _skillsList.Count; i++)
            {

                await _globalFactory.CreateSkillEdge(_skillsList[i], _edgesParent.transform);
            }
        }
    }
}