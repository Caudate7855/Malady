using UnityEngine;

namespace Project.Scripts
{
    public class PassiveSkillTreeScaler : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _zoomSpeed = 0.1f;   
        [SerializeField] private float _minScale = 0.5f;   
        [SerializeField] private float _maxScale = 2.0f;


        public Vector3 GetScale()
        {
            return _rectTransform.localScale;
        }
        
        private void Update()
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");

            if (Mathf.Abs(scroll) > 0.01f)
            {
                Vector3 scale = _rectTransform.localScale;

                scale += Vector3.one * scroll * _zoomSpeed;

                scale.x = Mathf.Clamp(scale.x, _minScale, _maxScale);
                scale.y = Mathf.Clamp(scale.y, _minScale, _maxScale);
                scale.z = 1;

                _rectTransform.localScale = scale;
            }
        }
    }
}