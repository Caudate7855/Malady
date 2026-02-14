using System;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts
{
    public class DropItemUIView : MonoBehaviour
    {
        [field: SerializeField] public Image Image;
        [field: SerializeField] private Button Button;

        public void SetOnClick(Action onClick)
        {
            Button.onClick.RemoveAllListeners();
            Button.onClick.AddListener(() =>
            {
                onClick?.Invoke();
            });
        }

        public void ClearOnClick()
        {
            Button.onClick.RemoveAllListeners();
        }

        public void SetInteractable(bool state)
        {
            Button.interactable = state;
        }
    }
}