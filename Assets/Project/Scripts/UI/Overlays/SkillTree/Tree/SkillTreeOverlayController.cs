using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Itibsoft.PanelManager;
using Project.Scripts.Overlays;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Project.Scripts.SkillTree
{
    [Panel(PanelType = PanelType.Overlay, Order = 0, AssetId = "PassiveSkillTreeOverlay")]
    public class SkillTreeOverlayController : PanelControllerBase<PassiveSkillTreeView>
    {
        [Inject] private GlobalFactory _globalFactory;
        [Inject] private IPanelManager _panelManager;
        
        private List<Skill> _skillsList = new();
        private GameObject _edgesParent;        
        private Button _skillListButton;
        
        private SpellListOverlayController _spellListOverlayController;
        
        protected async override void Initialize()
        {
            _skillsList = Panel.GetSkillsList();
            _edgesParent = Panel.ParentObject;
            _skillListButton = Panel.SkillListButton;
            
            _skillListButton.onClick.AddListener(OnSkillListButtonClick);
            
            await CreateSkillsEdges(_skillsList);
        }

        private void OnSkillListButtonClick()
        {
            _spellListOverlayController = _panelManager.LoadPanel<SpellListOverlayController>();
            _spellListOverlayController.Open();
            Close();
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