using System;
using Project.Scripts.Core.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core.Abstracts
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable, IApproachable
    {
        private PlayerController _playerController;
        public event Action OnApproach;

        public void Initialize(PlayerController playerController)
        {
            _playerController = playerController;
        }

        public abstract void Interact();

        private void Awake()
        {
            ChangeOutline(false);
        }

        private void OnMouseDown()
        {
            ApproachCharacter();
        }

        private void OnMouseEnter()
        {
            ChangeOutline(true);
        }

        private void OnMouseExit()
        {
            ChangeOutline(false);
        }

        void ChangeOutline(bool condition)
        {
            gameObject.GetComponent<Outline>().enabled = condition;
        }

        public void ApproachCharacter()
        {
            OnApproach += Interact;
            _playerController.OnDestinationApproach += (() => OnApproach?.Invoke());
            _playerController.MoveToLocation(gameObject.transform.position);
        }
    }
}