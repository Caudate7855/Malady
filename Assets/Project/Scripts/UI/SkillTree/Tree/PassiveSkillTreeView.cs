using System.Collections.Generic;
using System.Linq;
using Itibsoft.PanelManager;
using UnityEngine;

namespace Project.Scripts.SkillTree
{
    public class PassiveSkillTreeView : PanelBase
    {
        public List<Skill> SkillsList => _skillsList;
        
        [SerializeField] private List<Skill> _skillsList = new();

        public void CreateSkillsList()
        {
            _skillsList = GetComponentsInChildren<Skill>(true).ToList();
        }
    }
}