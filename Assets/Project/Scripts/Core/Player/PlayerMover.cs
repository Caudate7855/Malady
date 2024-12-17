using System;
using System.Collections;
using Project.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core
{
    public class PlayerMover : MonoBehaviour
    {
        public event Action OnDestinationReached;

        public NavMeshAgent NavMeshAgent;

        [SerializeField] private LayerMask _layerMask;

        private MouseController _mouseController;

        private Camera _mainCamera;
        private bool isRunning = false;
        

        private void Awake()
        {
            _mainCamera = FindObjectOfType<Camera>();
            NavMeshAgent = GetComponent<NavMeshAgent>();

            _mouseController.Initialize();
        }

        public void MoveToPoint()
        {
            StartCoroutine(DeferredCheckAndMove());
        }

        public void MoveToPoint(Vector3 location)
        {
            StartCoroutine(DeferredCheckAndMove(location));
        }

        private IEnumerator DeferredCheckAndMove(Vector3 location)
        {
            NavMeshAgent.SetDestination(location);
            isRunning = true;

            yield return null;
        }
        
        private IEnumerator DeferredCheckAndMove()
        {
            var mousePosition = _mouseController.GetMouseGroundPositionInWorld();

            NavMeshAgent.SetDestination(mousePosition);
            isRunning = true;

            yield return null;
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
                if (NavMeshAgent.remainingDistance <= 0)
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