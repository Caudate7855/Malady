using DG.Tweening;
using Project.Scripts.Core.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Core.Abstracts
{
    public abstract class InteractableZoneBase : MonoBehaviour, IInteractableZone, IInteractable
    {
        private const float FADE_DURATION = 0.2f;

        public float InteractionCooldownInSeconds { get; set; }
        public Button _interactionButton;
        
        private void Awake()
        {
            _interactionButton.interactable = false;
            var color = _interactionButton.GetComponent<Image>().color;
            color.a = 0;
            _interactionButton.GetComponent<Image>().color = color;


            _interactionButton.onClick.AddListener(Interact);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerMover>())
            {
                ShowInteractionButton();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PlayerMover>())
            {
                HideInteractionButton();
            }
        }

        public void ShowInteractionButton()
        {
            _interactionButton.gameObject.SetActive(true);

            _interactionButton.gameObject.GetComponent<Image>()
                .DOFade(1, FADE_DURATION)
                .OnComplete(() => _interactionButton.interactable = true);
        }

        public void HideInteractionButton()
        {
            _interactionButton.gameObject.GetComponent<Image>()
                .DOFade(0, FADE_DURATION)
                .OnComplete(CloseButton);
        }

        protected void CloseButton()
        {
            _interactionButton.interactable = false;
            _interactionButton.gameObject.SetActive(false);
        }

        public abstract void Interact();
    }
}