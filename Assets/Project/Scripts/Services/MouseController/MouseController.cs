using JetBrains.Annotations;
using Project.Scripts.Core.Abstracts;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class MouseController
    {
        private Camera _mainCamera;

        public void Initialize()
        {
            _mainCamera = Object.FindObjectOfType<Camera>();
        }

        public Vector3 GetMouseGroundPositionInWorld(out InteractableBase interactableBase)
        {
            interactableBase = null;

            if (EventSystem.current.IsPointerOverGameObject())
                return default;

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            int clickableLayerMask = LayerMask.GetMask("Clickable");
            if (Physics.Raycast(ray, out var hit, 100f, clickableLayerMask))
            {
                var go = hit.collider.gameObject;
                interactableBase = go.GetComponent<InteractableBase>();

                return go.transform.position;
            }

            int groundLayerMask = LayerMask.GetMask("Ground");
            if (Physics.Raycast(ray, out hit, 100f, groundLayerMask))
            {
                return hit.point;
            }

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