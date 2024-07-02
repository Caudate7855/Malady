using UnityEngine.InputSystem;

namespace Project.Scripts.Core
{
    public class PlayerInputController
    {
        private readonly IControllable _controllable;
        private readonly PlayerInputs _playerInputs = new PlayerInputs();

        public PlayerInputController(IControllable controllable)
        {
            _controllable = controllable;
            
            _playerInputs.Enable();
            _playerInputs.Gameplay.Movement.performed += OnMovementPerformed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            _controllable.MoveToPoint();
        }
    }
}