using UnityEngine;
using UnityEngine.AI;

namespace Project.Scripts.Core
{
    public class PlayerMover : MonoBehaviour, IPlayer, IControllable
    {
        [SerializeField] private LayerMask _layerMask;
        
        private Camera _mainCamera;
        private NavMeshAgent _navMeshAgent;

        private void Awake()
        {
            _mainCamera = FindObjectOfType<Camera>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }

        public void MoveInput()
        {
            MoveToPoint();
        }

        private void MoveToPoint()
        {
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray,out var raycastHit, 100, _layerMask))
            {
                _navMeshAgent.SetDestination(raycastHit.point);
            }
        }
    }
}