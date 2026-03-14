using System;
using Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public class EquipmentSystem : IInitializable, IDisposable
    {
        [Inject] private PlayerController _playerController;

        private Transform _armorContainer;
        private Transform _glovesContainer;
        private Transform _pantsContainer;
        private Transform _bootsContainer;
        
        public void Initialize()
        {
            _armorContainer = _playerController.ArmorContainer;
            _glovesContainer = _playerController.GlovesContainer;
            _pantsContainer = _playerController.PantsContainer;
            _bootsContainer = _playerController.BootsContainer;
        }

        public void EquipItem()
        {
            
        }

        public void RemoveItem()
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}