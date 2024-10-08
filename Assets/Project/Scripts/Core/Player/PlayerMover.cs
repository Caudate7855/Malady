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
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _mainCamera = FindObjectOfType<Camera>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
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
                _navMeshAgent.SetDestination(raycastHit.point);
            }
        }
    }
}