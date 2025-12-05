using System.Collections.Generic;
using System.Linq;
using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class PassiveSkillTreeView : PanelBase
    {
        public GameObject ParentObject => _parentObject;
        public Button SkillListButton => _skillListButton;
        public Button CloseButton => _closeButton;
        
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private List<Skill> _skillsList = new();
        
        [SerializeField] private Button _skillListButton;
        [SerializeField] private Button _closeButton;

        public List<Skill> GetSkillsList()
        {
            return GetComponentsInChildren<Skill>(true).ToList();
        }
    }
}