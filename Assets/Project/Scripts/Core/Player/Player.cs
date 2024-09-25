using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class Player : MonoBehaviour, IPlayer, ICustomInitializable, IControllable
    {
        private PlayerMover _playerMover;
        private IStatSystem _statSystem;

        public void Initialize()
        {
            _playerMover = GetComponent<PlayerMover>();
        }

        public void InitializeDependencies(IStatSystem statSystem)
        {
            _statSystem = statSystem;    
            _statSystem.DefaultInitialize();
        }

        public void MoveToPoint()
        {
            _playerMover.MoveToPoint();
        }
    }
}