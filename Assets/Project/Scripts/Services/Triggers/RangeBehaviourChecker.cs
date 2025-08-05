using System;
using UnityEngine;

namespace Project.Scripts
{
    public class RangeBehaviourChecker : MonoBehaviour
    {
        public string _name;
        public float _checkDistanceRadius = 5f;
        public Color _gizmoColor = Color.red;
        public event Action OnTriggerEnterEvent;
        public event Action OnTriggerExitEvent;

        private bool _isInitialized;
        private GameObject _objectToCheck;

        private bool _isObjectEntered;

        public void Initialize(MonoBehaviour objectToCheck)
        {
            _objectToCheck = objectToCheck.gameObject;
            
            _isInitialized = true;
            
            InvokeRepeating(nameof(CheckDistance), 1f,1f);
        }
        
        private void CheckDistance()
        {
            if (!_isInitialized)
                return;
            
            if (Vector3.Distance(transform.position, _objectToCheck.transform.position) < _checkDistanceRadius)
            {
                if(_isObjectEntered)
                    return;
                
                _isObjectEntered = true;
                OnTriggerEnterEvent?.Invoke();
            }
            else
            {
                
                if(!_isObjectEntered)
                    return;
                
                _isObjectEntered = false;
                OnTriggerExitEvent?.Invoke();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _checkDistanceRadius);
        }
    }
}