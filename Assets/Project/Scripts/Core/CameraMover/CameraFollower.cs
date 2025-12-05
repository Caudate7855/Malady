using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class CameraFollower : MonoBehaviour
    {
        [Inject] private PlayerController _playerController;
        
        private readonly Vector3 _cameraOffsetPosition = new (-4, 10, -4);
        private readonly Vector3 _cameraOffsetRotation = new (60, 45, 0);

        private void Update()
        {
            transform.position = _playerController.transform.position + _cameraOffsetPosition;

            var rotation = transform.rotation;
            rotation.eulerAngles = _cameraOffsetRotation;
            transform.rotation = rotation;
        }
    }
}