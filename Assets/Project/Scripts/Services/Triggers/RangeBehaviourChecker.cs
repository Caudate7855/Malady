using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Project.Scripts
{
    public class RangeBehaviourChecker : MonoBehaviour
    {
        public string _name;

        [SerializeField] private float _checkDistanceRadius = 5f;
        [SerializeField] private Color _gizmoColor = Color.red;
        [SerializeField] private LayerMask _layerMask = ~0;

        public event Action OnTriggerEnterEvent;
        public event Action OnTriggerExitEvent;

        private Type _followType;
        private bool _isInitialized;
        private bool _isObjectEntered;

        public void Initialize<T>() where T : MonoBehaviour
        {
            _followType = typeof(T);
            _isInitialized = true;
            InvokeRepeating(nameof(CheckDistance), 1f, 1f);
        }

        private void CheckDistance()
        {
            if (!_isInitialized)
                return;

            Collider[] hits = Physics.OverlapSphere(transform.position, _checkDistanceRadius, _layerMask);
            bool found = false;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i] == null) continue;
                if (hits[i].TryGetComponent(_followType, out _))
                {
                    found = true;
                    break;
                }
            }

            if (found && !_isObjectEntered)
            {
                _isObjectEntered = true;
                OnTriggerEnterEvent?.Invoke();
            }
            else if (!found && _isObjectEntered)
            {
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