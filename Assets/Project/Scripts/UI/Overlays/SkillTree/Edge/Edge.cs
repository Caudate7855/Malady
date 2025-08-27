using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    public class Edge : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;
        [SerializeField] private RectTransform _imageRectTransform;

        public Skill _firstSkill;
        public Skill _secondSkill;

        public void Initialize(float width, Skill firstSkill, Skill secondSkill, bool isEnabled = false)
        {
            SetWidth(width);
            
            _firstSkill =  firstSkill;
            _secondSkill = secondSkill;

            if (isEnabled)
            {
                Enable();
            }
            else
            {
                Disable();
            }
        }
        
        public void SetWidth(float newWidth)
        {
            _imageRectTransform.sizeDelta = new Vector2(newWidth, _imageRectTransform.sizeDelta.y);
        }

        public void Enable()
        {
            _image.sprite = _enabledSprite;
        }

        public void Disable()
        {
            _image.sprite = _disabledSprite;
        }
    }
}