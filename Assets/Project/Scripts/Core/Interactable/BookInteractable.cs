using Project.Scripts.Core.Abstracts;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class BookInteractable : InteractableBase
    {
        public override void Interact()
        {
            Debug.Log("INTERACT");
        }
    }
}