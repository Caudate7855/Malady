using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Camera _mainCamera;
        
        private NavMeshAgent _navMeshAgent;
        [SerializeField] private NavMeshSurface _navMeshSurface;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshSurface.BuildNavMesh();
        }

        public void Move()
        {
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out var raycastHit, 100, _layerMask))
            {
                _navMeshAgent.SetDestination(raycastHit.point);
            }
        }
    }
}