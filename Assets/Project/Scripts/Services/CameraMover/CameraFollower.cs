using Project.Scripts.Walls;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class CameraFollower : MonoBehaviour
    {
        [Inject] private PlayerController _playerController;

        [SerializeField] private LayerMask _raycastMask;
        [SerializeField] private float _raycastInterval = 0.2f;

        private readonly Vector3 _cameraOffsetPosition = new(-4, 10, -4);
        private readonly Vector3 _cameraOffsetRotation = new(60, 45, 0);

        private DungeonWall _currentWall;
        private Camera _camera;

        private void Start()
        {
            InvokeRepeating(nameof(RaycastToPlayer), 0f, _raycastInterval);
        
            _camera = GetComponent<Camera>();
            _camera.backgroundColor = Color.black;
        }

        private void Update()
        {
            transform.position = _playerController.transform.position + _cameraOffsetPosition;

            var rotation = transform.rotation;
            rotation.eulerAngles = _cameraOffsetRotation;
            transform.rotation = rotation;
        }

        private void RaycastToPlayer()
        {
            var origin = transform.position;
            var target = _playerController.transform.position;
            var direction = target - origin;
            var distance = direction.magnitude;

            if (Physics.Raycast(origin, direction, out var hit, distance, _raycastMask))
            {
                if (hit.collider.TryGetComponent<DungeonWall>(out var wall))
                {
                    if (_currentWall != wall)
                    {
                        ClearCurrentWall();
                        _currentWall = wall;
                        _currentWall.SetUpperPartTransparent(false);
                    }

                    return;
                }
            }

            ClearCurrentWall();
        }

        private void ClearCurrentWall()
        {
            if (_currentWall == null)
            {
                return;
            }

            _currentWall.SetUpperPartTransparent(true);
            _currentWall = null;
        }
    }
}