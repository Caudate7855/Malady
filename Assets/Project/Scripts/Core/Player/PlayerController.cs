using Project.Scripts.FSM;
using Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

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

            _playerMover.OnDestinationReached += Idle;
        }

        public void InitializeDependencies(IStatSystem statSystem)
        {
            _statSystem = statSystem;    
            _statSystem.DefaultInitialize();
        }

        public void MoveToPoint()
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (_playerFsm.IsPossibleToMove)
            {
                ContinueMovement();
                _playerMover.MoveToPoint();
                _playerFsm.SetState<PlayerFsmStateRun>();
            }
        }

        public void StopMovement()
        {
            _playerMover.NavMeshAgent.isStopped = true;
        }

        public void ContinueMovement()
        {
            _playerMover.NavMeshAgent.isStopped = false;
        }

        public void Idle()
        {
            _playerFsm.SetState<PlayerFsmStateIdle>();
        }
        
        public void Cast()
        {
            StopMovement();
            _playerFsm.SetState<PlayerFsmStateCast>();
        }

        public void Summon()
        {
            StopMovement();
            _playerFsm.SetState<PlayerFsmStateSummon>();
        }
    }
}