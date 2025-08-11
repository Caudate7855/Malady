using UnityEngine;

namespace Project.Scripts.SkillTree
{
    public class Edge : MonoBehaviour
    {
        [SerializeField] private Skill _startedSkill;
        [SerializeField] private Skill _secondSkill;


        public Skill GetStartedSkill()
        {
            return _startedSkill;
        }

        public Skill GetSecondSkill()
        {
            return _secondSkill;
        }
    }
}