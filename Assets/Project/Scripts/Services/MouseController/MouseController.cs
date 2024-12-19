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

        public Vector3 GetMouseGroundPositionInWorld()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return default;
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var raycastHit, 100))
            {
                if (raycastHit.collider.gameObject.GetComponent<InteractableBase>())
                {
                    return raycastHit.collider.gameObject.transform.position;
                }
                
                return raycastHit.point;
            }

            return default;
        }
    }
}