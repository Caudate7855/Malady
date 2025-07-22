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
        private const float MAX_REACH_DISTANCE = 5.0f;
        public event Action OnDestinationReached;

        private NavMeshAgent _navMeshAgent;
        private bool _isSetted;
        private bool _isMoving = false;

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
                return;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(location, out hit, MAX_REACH_DISTANCE, NavMesh.AllAreas))
            {
                _isMoving = true;
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

        private void HasReachedDestination()
        {
            if (!_isSetted || !_isMoving)
                return;

            if (!_navMeshAgent.pathPending)
            {
                if (_navMeshAgent.remainingDistance <= 0)
                {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        _isMoving = false; // сбрасываем
                        OnDestinationReached?.Invoke();
                    }
                }
            }
        }
    }
}