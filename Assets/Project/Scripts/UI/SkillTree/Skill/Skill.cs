using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.SkillTree
{
    public class Skill : MonoBehaviour
    {
        public bool IsEnabled;

        [SerializeField] private List<Skill> _linkedSkills = new();
        [SerializeField] private List<Edge> _linkedEdges = new();
        
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnPassiveSkillPressed);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnPassiveSkillPressed);
        }

        private void OnPassiveSkillPressed()
        {
            var edge = FindEdgeWithLinkedSkill();
            
            if (!IsEnabled)
            {
                IsEnabled = true;

                if (edge != null)
                {
                    edge.Enable();
                }
            }
            else
            {
                IsEnabled = false;

                if (edge != null)
                {
                    edge.Disable();
                }
            }
        }

        private Edge FindEdgeWithLinkedSkill()
        {
            for (int i = 0, count = _linkedSkills.Count; i < count; i++)
            {
                if (_linkedSkills[i].IsEnabled)
                {
                    for (int j = 0; j < _linkedEdges.Count; j++)
                    {
                        for (int k = 0; k < _linkedSkills[i]._linkedEdges.Count; k++)
                        {
                            if (_linkedEdges[i] == _linkedSkills[i]._linkedEdges[k])
                            {
                                return _linkedEdges[i];
                            }                            
                        }
                    }
                }
            }
            
            return null;
        }
        
        
        public List<Skill> GetLinkedSkills()
        {
            return _linkedSkills;
        }

        public List<Edge> GetLinkedEdges()
        {
            return _linkedEdges;
        }
    }
}