using UnityEngine;

namespace Project.Scripts.SkillTree
{
    public class Edge : MonoBehaviour
    {
        public Skill StartedSkill;
        public Skill SecondSkill;
        
        [SerializeField] private RectTransform _imageRectTransform;

        public void SetWidth(float newWidth)
        {
            _imageRectTransform.sizeDelta = new Vector2(newWidth, _imageRectTransform.sizeDelta.y);
        }
    }
}