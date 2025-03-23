using System;
using UnityEngine;

namespace Project.Scripts
{
    public class TriggerChecker : MonoBehaviour
    {
        private const float CHECK_DISTANCE_RANGE = 5f;
        
        public bool IsStayInTrigger;
        public event Action OnTriggerEnterEvent;
        public event Action OnTriggerExitEvent;

        private MonoBehaviour _targetObject;
        private bool _isInitialized;

        public void Initialize(MonoBehaviour targetObject)
        {
            _targetObject = targetObject;
            
            _isInitialized = true;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!_isInitialized)
            {
                return;
            }

            if (other.GetComponent(_targetObject.GetType()))
            {
                Debug.Log($"Enter trigger {other.name}");
                OnTriggerEnterEvent?.Invoke();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (_isInitialized)
            {
                return;
            }
            
            IsStayInTrigger = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_isInitialized)
            {
                return;
            }
            
            IsStayInTrigger = false;
            OnTriggerExitEvent?.Invoke();
        }
        
        private void CheckDistance(GameObject targetObject)
        {
            if (Vector3.Distance(transform.position, targetObject.transform.position) > CHECK_DISTANCE_RANGE)
            {
                
            }
        }
    }
}