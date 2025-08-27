using System.Collections.Generic;
using System.Linq;
using Itibsoft.PanelManager;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.SkillTree
{
    public class PassiveSkillTreeView : PanelBase
    {
        public GameObject ParentObject => _parentObject;
        public Button SkillListButton => _skillListButton;
        
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private List<Skill> _skillsList = new();
        
        [SerializeField] private Button _skillListButton;

        public List<Skill> GetSkillsList()
        {
            return GetComponentsInChildren<Skill>(true).ToList();
        }
    }
}