using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.UI
{
    [RequireComponent(typeof(Slider))]
    public class BarBase : MonoBehaviour, IBar
    {
        private Slider Slider;
        private void Awake()
        {
            Slider = GetComponent<Slider>();
        }

        public void Initialize(float maxValue)
        {
            Slider.maxValue = maxValue;            
        }

        public void UpdateBar(float newValue)
        {
            Slider.value = newValue;
        }
    }
}