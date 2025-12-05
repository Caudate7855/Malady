using UnityEngine;

namespace Project.Scripts
{
    public class CameraFollower : MonoBehaviour
    {
        public bool IsInitialized;

        private readonly Vector3 _cameraOffsetPosition = new Vector3(-4, 10, -4);
        private readonly Vector3 _cameraOffsetRotation = new Vector3(60, 45, 0);

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