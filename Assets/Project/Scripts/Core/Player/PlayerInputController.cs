using Itibsoft.PanelManager;
using Project.Scripts.Overlays;
using Project.Scripts.Overlays.Inventory;
using UnityEngine.InputSystem;

namespace Project.Scripts.Core
{
    public class PlayerInputController
    {
        private IMovable _movable;
        private ICastable _castable;

        private InventoryController _inventoryController;
        private MainUIController _mainUIController;

        private readonly PlayerInputs _playerInputs = new();

        private bool _isInventoryOpened;

        public void Initialize(IMovable movable, ICastable castable, IPanelManager panelManager)
        {
            _movable = movable;
            _castable = castable;

            _playerInputs.Enable();

            _inventoryController = panelManager.LoadPanel<InventoryController>();
            _mainUIController = panelManager.LoadPanel<MainUIController>();

            _playerInputs.Gameplay.Inventory.performed += OnInventoryPerformed;

            _playerInputs.Gameplay.PlayerSpell1.performed += OnPlayerSpellPerformed0;
            _playerInputs.Gameplay.PlayerSpell2.performed += OnPlayerSpellPerformed1;
            _playerInputs.Gameplay.PlayerSpell3.performed += OnPlayerSpellPerformed2;
            _playerInputs.Gameplay.PlayerSpell4.performed += OnPlayerSpellPerformed3;

            _playerInputs.Gameplay.SummonSpell1.performed += OnSummonSpellPerformed0;
            _playerInputs.Gameplay.SummonSpell2.performed += OnSummonSpellPerformed1;
            _playerInputs.Gameplay.SummonSpell3.performed += OnSummonSpellPerformed2;
            _playerInputs.Gameplay.SummonSpell4.performed += OnSummonSpellPerformed3;
        }


        public void Update()
        {
            if (_playerInputs.Gameplay.Movement.inProgress)
            {
                _movable.MoveToPoint();
            }
        }
        
        private void OnMovementPressed(bool condition)
        {
            while (condition)
            {
                _movable.MoveToPoint();
            }
        }

        private void OnMovementPerformed(InputAction.CallbackContext obj)
        {
            _movable.MoveToPoint();
        }

        private void OnInventoryPerformed(InputAction.CallbackContext obj)
        {
            if (_isInventoryOpened)
            {
                _inventoryController.Close();
                _isInventoryOpened = false;
            }
            else
            {
                _inventoryController.Open();
                _isInventoryOpened = true;
            }
        }

        private void OnPlayerSpellPerformed0(InputAction.CallbackContext obj)
        {
            _mainUIController.OnPlayerSpellButtonClicked(0);
            _castable.Cast();
        }

        private void OnPlayerSpellPerformed1(InputAction.CallbackContext obj)
        {
            _mainUIController.OnPlayerSpellButtonClicked(1);
            _castable.Summon();
        }

        private void OnPlayerSpellPerformed2(InputAction.CallbackContext obj)
        {
            _mainUIController.OnPlayerSpellButtonClicked(2);
        }

        private void OnPlayerSpellPerformed3(InputAction.CallbackContext obj)
        {
            _mainUIController.OnPlayerSpellButtonClicked(3);
        }

        private void OnSummonSpellPerformed0(InputAction.CallbackContext obj)
        {
            _mainUIController.OnSummonSpellButtonClicked(0);
        }

        private void OnSummonSpellPerformed1(InputAction.CallbackContext obj)
        {
            _mainUIController.OnSummonSpellButtonClicked(1);
        }

        private void OnSummonSpellPerformed2(InputAction.CallbackContext obj)
        {
            _mainUIController.OnSummonSpellButtonClicked(2);
        }

        private void OnSummonSpellPerformed3(InputAction.CallbackContext obj)
        {
            _mainUIController.OnSummonSpellButtonClicked(3);
        }
    }
}