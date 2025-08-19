using UnityEngine;

namespace Project.Scripts
{
    public class LookAtCameraUIElement : MonoBehaviour
    {
        private Camera _mainCamera;

        private void Awake()
        {
            _mainCamera = FindFirstObjectByType<Camera>();

            var canvas = GetComponentInParent<Canvas>();
            canvas.worldCamera = _mainCamera;
        }

        private void Update()
        {
            if (_mainCamera != null)
            {
                transform.LookAt(transform.position + _mainCamera.transform.rotation * Vector3.forward,
                    _mainCamera.transform.rotation * Vector3.up);
            }
        }
    }
}