using Project.Scripts.FSM;
using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class PlayerController : MonoBehaviour, IPlayer, ICustomInitializable, IMovable, ICastable
    {
        private PlayerMover _playerMover;
        private IStatSystem _statSystem;
        private PlayerFsm _playerFsm;
        
        public void Initialize()
        {
            _playerMover = GetComponent<PlayerMover>();
            _playerFsm = GetComponent<PlayerFsm>();
        }

        public void InitializeDependencies(IStatSystem statSystem)
        {
            _statSystem = statSystem;    
            _statSystem.DefaultInitialize();
        }

        public void MoveToPoint()
        {
            _playerMover.MoveToPoint();
            _playerFsm.SetState<PlayerFsmStateWalk>();
        }

        public void Cast()
        {
            _playerFsm.SetState<PlayerFsmStateCast>();
        }

        private void Update()
        {
            if (_playerMover.NavMeshAgent.velocity.magnitude <= 0.1f)
            {
                _playerFsm.SetState<PlayerFsmStateIdle>();
            }
        }
    }
}