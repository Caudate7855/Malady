using Project.Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Core.Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private IControllable _controllable;
        private PlayerInputs _playerInputs;

        private void Awake()
        {
            _playerInputs = new PlayerInputs();
            _playerInputs.Enable();

            _controllable = GetComponent<IControllable>();
        }

        private void OnEnable() 
        {
            _playerInputs.Gameplay.Movement.performed += OnMovementPerformed;
        }

        private void OnDisable()
        {
            _playerInputs.Gameplay.Movement.performed -= OnMovementPerformed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            _controllable.MoveInput();
        }
    }
}