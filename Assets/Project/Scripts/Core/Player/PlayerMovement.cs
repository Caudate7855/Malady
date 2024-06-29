using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Camera _mainCamera;
        
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void Move()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out var raycastHit, 100, _layerMask))
            {
                _navMeshAgent.SetDestination(raycastHit.point);
            }
        }
    }
}