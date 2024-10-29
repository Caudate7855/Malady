using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Project.Scripts.Core
{
    public class PlayerMover : MonoBehaviour, IControllable
    {
        [SerializeField] private LayerMask _layerMask;
        
        private Camera _mainCamera;
        public NavMeshAgent NavMeshAgent;

        private void Awake()
        {
            _mainCamera = FindObjectOfType<Camera>();
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveToPoint()
        {
            StartCoroutine(DeferredCheckAndMove());
        }
        
        private IEnumerator DeferredCheckAndMove()
        {
            yield return null;

            if (EventSystem.current.IsPointerOverGameObject())
            {
                yield break; 
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var raycastHit, 100, _layerMask))
            {
                NavMeshAgent.SetDestination(raycastHit.point);
            }
        }
    }
}