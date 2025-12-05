using System;
using R3;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class RangeBehaviourChecker : MonoBehaviour
    {
        public ReactiveProperty<(bool, GameObject)> IsInRange { get; } = new();

        [SerializeField] private float _checkDistanceRadius = 5f;
        [SerializeField] private Color _gizmoColor = Color.red;

        private Type _followType;
        private bool _isInitialized;
        private GameObject _targetObject;

        public void Initialize<T>() where T : MonoBehaviour
        {
            _followType = typeof(T);
            _isInitialized = true;
            InvokeRepeating(nameof(CheckDistance), 0.1f, 0.1f);
        }

        private void CheckDistance()
        {
            if (!_isInitialized)
                return;

            Collider[] hits = Physics.OverlapSphere(transform.position, _checkDistanceRadius);
            GameObject foundObj = null;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].TryGetComponent(_followType, out _))
                {
                    foundObj = hits[i].gameObject;
                    break;
                }
            }

            if (foundObj != null)
            {
                _targetObject = foundObj;
                IsInRange.Value = (true, _targetObject);
            }
            else
            {
                _targetObject = null;
                IsInRange.Value = (false, null);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _gizmoColor;
            Gizmos.DrawWireSphere(transform.position, _checkDistanceRadius);
        }
    }
}