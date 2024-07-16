using Itibsoft.PanelManager;
using Project.Scripts.UI.Overlays.Inventory;
using UnityEngine.InputSystem;

namespace Project.Scripts.Core
{
    public class PlayerInputController
    {
        private readonly IControllable _controllable;
        private readonly InventoryController _inventoryController;
        private readonly PlayerInputs _playerInputs = new();

        public PlayerInputController(IControllable controllable, IPanelManager panelManager)
        {
            _controllable = controllable;

            _playerInputs.Enable();
            
            _inventoryController = panelManager.LoadPanel<InventoryController>();
            
            _playerInputs.Gameplay.Movement.performed += OnMovementPerformed;
            _playerInputs.Gameplay.Inventory.performed += OnInventoryPerformed;
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            _controllable.MoveToPoint();
        }

        private void OnInventoryPerformed(InputAction.CallbackContext obj)
        {
           _inventoryController.Open();
        }
    }
}