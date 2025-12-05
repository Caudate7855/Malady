using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class Skill : MonoBehaviour
    {
        public bool IsEnabled;

        [SerializeField] private bool _isStartedSkill;
        [SerializeField] private List<Skill> _linkedSkills = new();
        [SerializeField] private List<Edge> _linkedEdges = new();
        [SerializeField] private Image _closeOverlay;
        
        private Button _button;
        private Image _image;
        
        private readonly Color _enabledColor = new (0,0,0,0);
        private readonly Color _disabledColor = new (0,0,0,150);

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
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
                if (_isStartedSkill)
                {
                    EnableSkill();
                    return;
                }
                
                if (edge != null)
                {
                    EnableSkill();
                    edge.Enable();
                }
            }
            else
            {
                if (_isStartedSkill)
                {
                    DisableSkill();
                    return;
                }
                
                if (edge != null)
                {
                    DisableSkill();
                    edge.Disable();
                }
            }
        }

        private void EnableSkill()
        {
            IsEnabled = true;
            _closeOverlay.color = _enabledColor;
        }

        private void DisableSkill()
        {
            _closeOverlay.color = _disabledColor;
            IsEnabled = false;
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