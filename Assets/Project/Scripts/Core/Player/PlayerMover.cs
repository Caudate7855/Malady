using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Project.Scripts.Player
{
    public sealed class PlayerMover : ITickable
    {
        private const float MaxReachDistance = 5f;

        public event Action OnDestinationReached;

        private NavMeshAgent _navMeshAgent;
        private bool _isMoving;

        public void SetNavMeshAgent(NavMeshAgent navMeshAgent)
        {
            if (_navMeshAgent != null)
            {
                return;
            }

            _navMeshAgent = navMeshAgent;
        }

        public bool IsReady()
        {
            return _navMeshAgent != null && _navMeshAgent.isOnNavMesh;
        }

        public void StopMovement()
        {
            if (!IsReady())
            {
                return;
            }

            _isMoving = false;
            _navMeshAgent.isStopped = true;

            if (_navMeshAgent.hasPath)
            {
                _navMeshAgent.ResetPath();
            }

            _navMeshAgent.velocity = Vector3.zero;
        }

        public void ContinueMovement()
        {
            if (!IsReady())
            {
                return;
            }

            _navMeshAgent.isStopped = false;
        }

        public bool MoveToPoint(Vector3 location)
        {
            if (!IsReady())
            {
                return false;
            }

            if (!NavMesh.SamplePosition(location, out var hit, MaxReachDistance, NavMesh.AllAreas))
            {
                return false;
            }

            _isMoving = true;
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(hit.position);
            return true;
        }

        public void ClearOnDestinationReached()
        {
            OnDestinationReached = null;
        }

        public void Tick()
        {
            if (!_isMoving || !IsReady())
            {
                return;
            }

            if (_navMeshAgent.pathPending)
            {
                return;
            }

            if (_navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance)
            {
                return;
            }

            if (_navMeshAgent.hasPath && _navMeshAgent.velocity.sqrMagnitude > 0.001f)
            {
                return;
            }

            _isMoving = false;
            OnDestinationReached?.Invoke();
        }
    }
}
