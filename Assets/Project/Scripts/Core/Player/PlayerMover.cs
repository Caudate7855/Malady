using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core
{
    public class PlayerMover : MonoBehaviour
    {
        public event Action OnDestinationReached;
        
        public NavMeshAgent NavMeshAgent;

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveToPoint(Vector3 location)
        {
            StartCoroutine(DeferredCheckAndMove(location));
        }

        private IEnumerator DeferredCheckAndMove(Vector3 location)
        {
            NavMeshAgent.SetDestination(location);
            yield return null;
        }

        public void ClearOnDestinationReached()
        {
            OnDestinationReached = null;
        }
        
        
        void Update()
        {
            HasReachedDestination();
        }

        private bool HasReachedDestination()
        {
            if (!NavMeshAgent.pathPending)
            {
                if (NavMeshAgent.remainingDistance <= 0)
                {
                    if (!NavMeshAgent.hasPath || NavMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        OnDestinationReached?.Invoke();
                        return true;
                    }
                }
            }

            return false;
        }
    }
}