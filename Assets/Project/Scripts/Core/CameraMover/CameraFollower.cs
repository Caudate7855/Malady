using UnityEngine;

namespace Project.Scripts.Core
{
    public class CameraFollower : MonoBehaviour
    {
        public bool IsInitialized;

        [SerializeField] private Vector3 _cameraOffsetPosition;
        [SerializeField] private Vector3 _cameraOffsetRotation;

        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
            
            IsInitialized = true;
        }

        private void Update()
        {
            if (IsInitialized)
            {
                transform.position = _player.transform.position + _cameraOffsetPosition;
                
                var rotation = transform.rotation;
                rotation.eulerAngles = _cameraOffsetRotation;
                transform.rotation = rotation;
            }
        }
    }
}