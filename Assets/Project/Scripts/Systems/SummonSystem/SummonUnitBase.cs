using System;
using Project.Scripts.Core;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts
{
    public abstract class SummonUnitBase : MonoBehaviour, ICustomInitializable
    {
        public PlayerController PlayerControllerObject { get; set; }
        [SerializeField] private PlayerController _playerController;

        private void Start()
        {
            _playerController = PlayerControllerObject;
        }

        public void Initialize()
        {
            
        }
    }
}