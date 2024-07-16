using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class Player : MonoBehaviour, IPlayer, ICustomInitializable, IControllable
    {
        private PlayerMover _playerMover;
        
        public void Initialize()
        {
            _playerMover = GetComponent<PlayerMover>();
        }

        public void MoveToPoint()
        {
            _playerMover.MoveToPoint();
        }
    }
}