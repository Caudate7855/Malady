using System;
using Project.Scripts.FSM;
using Project.Scripts.Interfaces;
using Project.Scripts.Services;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Core
{
    public class PlayerController : MonoBehaviour, IPlayer, ICustomInitializable, IMovable, ICastable
    {
        public event Action OnDestinationApproach;
        public bool IsInteractableApproaching;

        private PlayerMover _playerMover;
        private IStatSystem _statSystem;
        private PlayerFsm _playerFsm;
        private PlayerStats _playerStats;

        private MouseController _mouseController;


        public void Initialize()
        {
            _playerMover = GetComponent<PlayerMover>();
            _playerFsm = GetComponent<PlayerFsm>();
            _mouseController.Initialize();

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

            if (_playerFsm.IsPossibleToMove && _mouseController.GetMouseGroundPositionInWorld() != default &&
                IsInteractableApproaching == false)
            {
                ContinueMovement();
                _playerMover.MoveToPoint();
                _playerFsm.SetState<PlayerFsmStateRun>();
            }
        }
        
        public void MoveToPoint(Vector3 targetLocation)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (_playerFsm.IsPossibleToMove &&
                IsInteractableApproaching == false)
            {
                ContinueMovement();
                _playerMover.MoveToPoint(targetLocation);
                _playerFsm.SetState<PlayerFsmStateRun>();
                _playerMover.OnDestinationReached += Approach;
            }
        }

        private void Approach()
        {
            IsInteractableApproaching = false;
            OnDestinationApproach?.Invoke();
        }

        private void StopMovement()
        {
            _playerMover.NavMeshAgent.isStopped = true;
            _playerMover.NavMeshAgent.velocity = Vector3.zero;
        }

        private void ContinueMovement()
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