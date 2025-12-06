using R3;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class CoreUpdater : MonoBehaviour
    {
        private const int ONE_SECOND_DELAY = 1;
        private const int FIVE_SECONDS_DELAY = 5;
        private const int TEN_SECONDS_DELAY = 10;

        public ReactiveCommand OnUpdatePerformed = new();
        public ReactiveCommand OnUpdateOnSecondPerformed = new();
        public ReactiveCommand OnUpdateOnFiveSecondsPerformed = new();
        public ReactiveCommand OnUpdateOnTenSecondsPerformed = new();

        private float _timeSinceLastSecondUpdate;
        private float _timeSinceLastFiveSecondUpdate;

        private void Update()
        {
            OnUpdatePerformed.Execute(default);

            _timeSinceLastSecondUpdate += Time.deltaTime;
            _timeSinceLastFiveSecondUpdate += Time.deltaTime;

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
            OnUpdateOnSecondPerformed.Execute(default);
        }

        private void UpdateOnFiveSeconds()
        {
            OnUpdateOnFiveSecondsPerformed.Execute(default);
        }
        
        private void UpdateOnTenSeconds()
        {
            OnUpdateOnTenSecondsPerformed.Execute(default);
        }
    }
}