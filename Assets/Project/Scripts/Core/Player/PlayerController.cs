using System;
using Project.Scripts.Core.Abstracts;
using Project.Scripts.FSM;
using Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.Scripts.Core
{
    public class PlayerController : MonoBehaviour, IPlayer, ICustomInitializable
    {
        private PlayerMover _playerMover;
        private IStatSystem _statSystem;
        private PlayerFsm _playerFsm;
        private PlayerStats _playerStats;

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
        
        public void MoveToPoint(Vector3 targetLocation, InteractableBase interactable = default)
        {
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }

            if (interactable != null)
            {
                _playerMover.OnDestinationReached += interactable.InteractWithCooldown;
            }
            
            if (_playerFsm.IsPossibleToMove)
            {
                ContinueMovement();
                _playerMover.MoveToPoint(targetLocation);
                _playerFsm.SetState<PlayerFsmStateRun>();
            }
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