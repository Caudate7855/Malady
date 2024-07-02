using UnityEngine.InputSystem;

namespace Project.Scripts.Core
{
    public class PlayerInputController
    {
        private IControllable _controllable;
        private PlayerInputs _playerInputs;

        public PlayerInputController(IControllable controllable)
        {
            _controllable = controllable;
            
            _playerInputs = new PlayerInputs();
            _playerInputs.Enable();
            Enable();
        }

        private void Enable() 
        {
            _playerInputs.Gameplay.Movement.performed += OnMovementPerformed;
        }

        private void Disable()
        {
            _playerInputs.Gameplay.Movement.performed -= OnMovementPerformed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            _controllable.MoveInput();
        }
    }
}