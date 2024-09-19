using UnityEngine;

namespace Project.Scripts.Core
{
    public class CameraFollower : MonoBehaviour
    {
        private Player _player;
        public bool IsInitialized;
        
        [SerializeField] private Vector3 _cameraOffsetPosition;
        [SerializeField] private Vector3 _cameraOffsetRotation;


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