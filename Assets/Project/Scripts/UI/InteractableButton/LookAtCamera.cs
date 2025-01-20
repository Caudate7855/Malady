using System;
using UnityEngine;

namespace Project.Scripts
{
    public class LookAtCamera : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = FindObjectOfType<Camera>();
        }

        void Update()
        {
            if (_mainCamera != null)
            {
                transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                    _mainCamera.transform.rotation * Vector3.up);
            }
        }
    }
}