using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(Slider))]
    public class BarBase : MonoBehaviour, IBar
    {
        private Slider _slider;
        
        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void Initialize(float maxValue)
        {
            _slider.maxValue = maxValue;            
        }

        public void UpdateBar(float newValue)
        {
            _slider.value = newValue;
        }
    }
}