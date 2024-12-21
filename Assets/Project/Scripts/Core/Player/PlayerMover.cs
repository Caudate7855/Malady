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

        private float _maxSampleDistance = 5.0f;

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
            NavMeshHit hit;
            
            if (NavMesh.SamplePosition(location, out hit, _maxSampleDistance, NavMesh.AllAreas))
            {
                NavMeshAgent.SetDestination(hit.position);
            }

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