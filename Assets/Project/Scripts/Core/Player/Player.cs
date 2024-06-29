using System;
using Project.Scripts.Core.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core.Player
{
    public class Player : MonoBehaviour, IPlayer, IControllable
    {
        private PlayerMovement _playerMovement;

        private void Awake()
        {
            _playerMovement = GetComponent<PlayerMovement>();
        }

        public void MoveInput()
        {
            _playerMovement.Move();
        }
    }
}