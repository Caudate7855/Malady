using Project.Scripts.Core.Abstracts;
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

        public GameObject GetHoveredGameObject()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                return hit.collider.gameObject;
            }

            return default;
        }

        public Vector3 GetMouseGroundPositionInWorld()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return default;
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var raycastHit, 100, _layerMask))
            {
                if (raycastHit.collider.gameObject.GetComponent<InteractableBase>())
                {
                    return default;
                }
                
                return raycastHit.point;
            }

            return default;
        }
    }
}