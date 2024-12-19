using System;
using Cysharp.Threading.Tasks;
using Project.Scripts.Core.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core.Abstracts
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public float InteractionCooldownInSeconds { get; set; } = 3f;

        private bool _isPossibleInteract;
        
        public abstract void Interact();

        private void Awake()
        {
            _isPossibleInteract = true;
            ChangeOutline(false);
        }

        private void OnMouseEnter()
        {
            ChangeOutline(true);
        }

        private void OnMouseExit()
        {
            ChangeOutline(false);
        }

        public async void InteractWithCooldown()
        {
            if (_isPossibleInteract)
            {
                _isPossibleInteract = false;
                Interact();
                await UniTask.Delay((int)(InteractionCooldownInSeconds * 1000));
                _isPossibleInteract = true;
            }
        }

        void ChangeOutline(bool condition)
        {
            gameObject.GetComponent<Outline>().enabled = condition;
        }
    }
}