using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.SkillTree
{
    public class Skill : MonoBehaviour
    {
        [SerializeField] private List<Skill> _linkedSkills = new();
        [SerializeField] private List<Edge> _linkedEdges = new();
        
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