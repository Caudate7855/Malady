using Project.Scripts.Core.Abstracts;
using Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Scripts.Core
{
    public class PlayerController : MonoBehaviour, IPlayer, ICustomInitializable
    {
        [Inject] private PlayerMover _playerMover;
        [Inject] private PlayerFsm _playerFsm;
        
        private IStatSystem _statSystem;
        private PlayerStats _playerStats;

        public void Initialize()
        {
            _playerMover.SetNavMeshAgent(GetComponent<NavMeshAgent>());
            _playerMover.OnDestinationReached += Idle;
            
            _playerFsm.Initialize(GetComponent<NavMeshAgent>(), GetComponentInChildren<Animator>());
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
                _playerMover.OnDestinationReached += () => Interact(interactable);
            }
            else
            {
                _playerMover.ClearOnDestinationReached();
                _playerMover.OnDestinationReached += Idle;
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
            _playerMover.StopMovement();
        }

        private void ContinueMovement()
        {
            _playerMover.ContinueMovement();
        }

        private void Interact(InteractableBase interactable = default)
        {
            if (interactable != null)
            {
                interactable.InteractWithCooldown();
            }
        }
        
        private void Idle()
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