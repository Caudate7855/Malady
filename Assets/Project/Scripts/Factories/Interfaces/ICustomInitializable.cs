using Project.Scripts.Core;
using UnityEngine;

namespace Project.Scripts.Interfaces
{
    public interface ICustomInitializable
    {
        public PlayerController PlayerControllerObject { get; set; }
        public void Initialize();

        public void InitializePlayerController()
        {
            PlayerControllerObject = PlayerController.Instance;
        }
    }
}