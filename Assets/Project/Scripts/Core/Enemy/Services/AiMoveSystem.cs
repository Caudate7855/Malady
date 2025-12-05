using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts
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
            _navMeshAgent.ResetPath();
            
            _navMeshAgent.isStopped = true;
            _navMeshAgent.velocity = Vector3.zero;
        }

        public void ContinueMovement()
        {
            ResetPath();
            
            _navMeshAgent.isStopped = false;
            _navMeshAgent.updateRotation = true;
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

                if (distance > 2f)
                {
                    if (NavMesh.SamplePosition(target.position, out NavMeshHit hit, MAX_REACH_DISTANCE, NavMesh.AllAreas))
                    {
                        _navMeshAgent.SetDestination(hit.position);
                    }
                }
                else
                {
                    ResetPath();
                    break;
                }

                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        public void ResetPath()
        {
            if (_navMeshAgent.hasPath)
            {
                _navMeshAgent.ResetPath();
            }
        }

        public void RotateToPoint(Vector3 point)
        {
            _navMeshAgent.updateRotation = false;
            var direction = point - _navMeshAgent.transform.position;
            direction.y = 0;
            _navMeshAgent.transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}