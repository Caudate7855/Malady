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

        private bool isRunning = false;
        

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