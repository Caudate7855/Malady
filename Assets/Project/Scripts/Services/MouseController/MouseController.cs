using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Services
{
    public struct MouseController
    {
        private Camera _mainCamera;
        private LayerMask _layerMask;
        
        public void Initialize()
        {
            _mainCamera = Object.FindObjectOfType<Camera>();
            _layerMask = LayerMask.GetMask("Ground");
        }

        public Vector3 GetMousePositionInWorld()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return default;
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out var raycastHit, 100, _layerMask))
            {
                return raycastHit.point;
            }

            return default;
        }
    }
}