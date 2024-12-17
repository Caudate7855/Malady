using System;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class Interactable : MonoBehaviour
    {
        private void Awake()
        {
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

        void ChangeOutline(bool condition)
        {
            gameObject.GetComponent<Outline>().enabled = condition;
        }
    }
}