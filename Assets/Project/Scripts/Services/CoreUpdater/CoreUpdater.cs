using System;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class CoreUpdater : MonoBehaviour
    {
        private const int ONE_SECOND_DELAY = 1;
        private const int FIVE_SECONDS_DELAY = 5;
        private const int TEN_SECONDS_DELAY = 10;

        public event Action OnUpdatePerformed;
        public event Action OnUpdateOnSecondPerformed;
        public event Action OnUpdateOnFiveSecondsPerformed;
        public event Action OnUpdateOnTenSecondsPerformed;

        private float _timeSinceLastSecondUpdate;
        private float _timeSinceLastFiveSecondUpdate;

        private void Update()
        {
            OnUpdatePerformed?.Invoke();

            var deltaTime = Time.deltaTime;
            
            _timeSinceLastSecondUpdate += deltaTime;
            _timeSinceLastFiveSecondUpdate += deltaTime;

            if (_timeSinceLastSecondUpdate >= ONE_SECOND_DELAY)
            {
                _timeSinceLastSecondUpdate -= ONE_SECOND_DELAY;
                UpdateOnSecond();
            }

            if (_timeSinceLastFiveSecondUpdate >= FIVE_SECONDS_DELAY)
            {
                _timeSinceLastFiveSecondUpdate -= FIVE_SECONDS_DELAY;
                UpdateOnFiveSeconds();
            }
            
            if (_timeSinceLastFiveSecondUpdate >= TEN_SECONDS_DELAY)
            {
                _timeSinceLastFiveSecondUpdate -= FIVE_SECONDS_DELAY;
                UpdateOnTenSeconds();
            }
        }

        private void UpdateOnSecond()
        {
            OnUpdateOnSecondPerformed?.Invoke();
        }

        private void UpdateOnFiveSeconds()
        {
            OnUpdateOnFiveSecondsPerformed?.Invoke();
        }
        
        private void UpdateOnTenSeconds()
        {
            OnUpdateOnTenSecondsPerformed?.Invoke();
        }
    }
}