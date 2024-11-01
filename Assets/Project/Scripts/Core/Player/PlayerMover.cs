using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Project.Scripts.Core
{
    public class PlayerMover : MonoBehaviour, IMovable
    {
        public event Action OnDestinationReached;
        
        public NavMeshAgent NavMeshAgent;

        [SerializeField] private LayerMask _layerMask;

        private Camera _mainCamera;
        private bool isRunning = false;

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
                isRunning = true;
            }
        }
        
        void Update()
        {
            if (HasReachedDestination() && isRunning)
            {
                isRunning = false;
                OnDestinationReached?.Invoke();
            }
        }

        private bool HasReachedDestination()
        {
            if (!NavMeshAgent.pathPending)
            {
                if (NavMeshAgent.remainingDistance <= NavMeshAgent.stoppingDistance)
                {
                    if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}