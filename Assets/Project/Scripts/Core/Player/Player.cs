using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class Player : MonoBehaviour, IPlayer, ICustomInitializable, IControllable
    {
        private PlayerInputController _playerInputController;
        private PlayerMover _playerMover;
        
        public void Initialize()
        {
            _playerInputController = new(this);
            _playerMover = GetComponent<PlayerMover>();
        }

        public void MoveToPoint()
        {
            _playerMover.MoveToPoint();
        }
    }
}