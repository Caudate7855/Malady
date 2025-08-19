using System.Collections.Generic;
using System.Linq;
using Itibsoft.PanelManager;
using UnityEngine;

namespace Project.Scripts.SkillTree
{
    public class PassiveSkillTreeView : PanelBase
    {
        public GameObject ParentObject => _parentObject;
        
        [SerializeField] private GameObject _parentObject;
        [SerializeField] private List<Skill> _skillsList = new();

        public List<Skill> GetSkillsList()
        {
            return GetComponentsInChildren<Skill>(true).ToList();
        }
    }
}