using System.Collections.Generic;
using System.Linq;
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

        public void AddEdge(Edge newEdge)
        {
            _linkedEdges.Add(newEdge);
        }

        private void OnPassiveSkillPressed()
        {
            var edge = FindEdgeWithLinkedSkill();
            
            if (!IsEnabled)
            {
                IsEnabled = true;

                if (edge != null)
                {
                    Debug.Log(edge.gameObject.name);
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
            foreach (var skill in _linkedSkills)
            {
                Debug.Log($"{name} checking neighbor {skill.name}, enabled={skill.IsEnabled}");
        
                if (!skill.IsEnabled) 
                    continue;

                var commonEdge = _linkedEdges.Intersect(skill._linkedEdges).FirstOrDefault();
                if (commonEdge != null)
                {
                    Debug.Log($"{name} found common edge {commonEdge.name} with {skill.name}");
                    return commonEdge;
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