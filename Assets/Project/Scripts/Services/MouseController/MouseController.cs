using Project.Scripts.Core.Abstracts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Services
{
    public class MouseController
    {
        private Camera _mainCamera;

        public void Initialize()
        {
            _mainCamera = Object.FindObjectOfType<Camera>();
        }

        public Vector3 GetMouseGroundPositionInWorld(out InteractableBase interactableBase)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                interactableBase = default;
                return default;
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100))
            {
                if (hit.collider.gameObject.GetComponent<InteractableBase>())
                {
                    interactableBase = hit.collider.gameObject.GetComponent<InteractableBase>();
                    return hit.collider.gameObject.transform.position;
                }

                interactableBase = default;
                return hit.point;
            }

            interactableBase = default;
            return default;
        }
        
        public GameObject GetFirstGameObjectUnderMouse()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return default;
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hit, 100))
            {
                return hit.collider.gameObject;
            }

            return default;
        }
        
        public Vector3 GetGameObjectPositionUnderMouse()
        {
            return GetFirstGameObjectUnderMouse().transform.position;
        }
    }
}