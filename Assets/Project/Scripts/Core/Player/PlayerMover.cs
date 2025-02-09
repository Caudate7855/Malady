using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts.Core
{
    [UsedImplicitly]
    public class PlayerMover : ITickable
    {
        public event Action OnDestinationReached;

        private NavMeshAgent _navMeshAgent;
        private float _maxSampleDistance = 5.0f;
        private bool _isSetted;

        public void SetNavMeshAgent(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
            _isSetted = true;
        }

        public void StopMovement()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero;
        }

        public void ContinueMovement()
        {
            _navMeshAgent.isStopped = false;
        }
        
        public async void MoveToPoint(Vector3 location)
        {
            if (_navMeshAgent == null)
            {
                return;
            }
            
            NavMeshHit hit;
            
            if (NavMesh.SamplePosition(location, out hit, _maxSampleDistance, NavMesh.AllAreas))
            {
                _navMeshAgent.SetDestination(hit.position);
            }

            await UniTask.WaitForEndOfFrame();
        }

        public void ClearOnDestinationReached()
        {
            OnDestinationReached = null;
        }
        
        public void Tick()
        {
            HasReachedDestination();
        }

        private bool HasReachedDestination()
        {
            if (!_isSetted)
            {
                return false;
            }
            
            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= 0)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
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