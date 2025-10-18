using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core
{
    public class AiMoveSystem
    {
        private const float MAX_REACH_DISTANCE = 3.0f;
        private NavMeshAgent _navMeshAgent;

        public void SetNavMeshAgent(NavMeshAgent navMeshAgent)
        {
            _navMeshAgent = navMeshAgent;
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

            ContinueMovement();
            NavMeshHit hit;
            
            if (NavMesh.SamplePosition(location, out hit, MAX_REACH_DISTANCE, NavMesh.AllAreas))
            {
                _navMeshAgent.SetDestination(hit.position);
            }
            
            await UniTask.WaitForEndOfFrame();
        }
        
        public async void FollowTarget(Transform target)
        {
            if (_navMeshAgent == null || target == null)
            {
                return;
            }

            ContinueMovement();
            
            while (target != null)
            {
                float distance = Vector3.Distance(_navMeshAgent.transform.position, target.position);

                if (distance > 1.5f)
                {
                    NavMeshHit hit;
                    if (NavMesh.SamplePosition(target.position, out hit, MAX_REACH_DISTANCE, NavMesh.AllAreas))
                    {
                        _navMeshAgent.SetDestination(hit.position);
                    }
                }
                else
                {
                    _navMeshAgent.ResetPath();
                }

                await UniTask.Yield();
            }
        }
    }
}