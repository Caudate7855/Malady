using UnityEngine;

namespace Project.Scripts.Core
{
    public class CameraFollower : MonoBehaviour
    {
        public bool IsInitialized;

        [SerializeField] private Vector3 _cameraOffsetPosition;
        [SerializeField] private Vector3 _cameraOffsetRotation;

        private PlayerController _playerController;

        public void Initialize(PlayerController playerController)
        {
            _playerController = playerController;
            
            IsInitialized = true;
        }

        private void Update()
        {
            if (IsInitialized)
            {
                transform.position = _playerController.transform.position + _cameraOffsetPosition;
                
                var rotation = transform.rotation;
                rotation.eulerAngles = _cameraOffsetRotation;
                transform.rotation = rotation;
            }
        }
    }
}