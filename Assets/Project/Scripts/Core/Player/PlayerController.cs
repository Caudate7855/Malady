using Itibsoft.PanelManager;
using Project.Scripts.Abstracts;
using Project.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Scripts
{
    public class PlayerController : MonoBehaviour, IInitializable
    {
        [Inject] private IPanelManager _panelManager;
        [Inject] private PlayerMover _playerMover;
        [Inject] private PlayerFsm _playerFsm;
        [Inject] private PlayerStats _playerStats;
        [Inject] private MouseController _mouseController;

        private MainUIController  _mainUIController;
        private NavMeshAgent _navMeshAgent;

        public void Initialize()
        {
            _navMeshAgent =  GetComponent<NavMeshAgent>();
            
            _mainUIController = _panelManager.LoadPanel<MainUIController>();
            
            _playerMover.OnDestinationReached += Idle;

            _playerFsm.Initialize(_navMeshAgent, GetComponentInChildren<Animator>());
            _playerStats.Initialize();
        }

        public bool IsNavMeshAgentReady()
        {
            return _navMeshAgent.enabled;
        }

        public void TryMoveToPoint(Vector3 targetLocation, InteractableBase interactable = default)
        {
            _playerMover.SetNavMeshAgent(GetComponent<NavMeshAgent>());
            
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
                _playerFsm.GetCurrentState().Update();
            }
        }

        private void StopMovement()
        {
            _playerMover.SetNavMeshAgent(GetComponent<NavMeshAgent>());
            _playerMover.StopMovement();
        }

        private void ContinueMovement()
        {
            _playerMover.SetNavMeshAgent(GetComponent<NavMeshAgent>());
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

        public void PlayCastAnimation(PlayerCastAnimations animationType)
        {
            StopMovement();

            var dir = _mouseController.GetGroundPosition() - transform.position;
            dir.y = 0f;

            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(dir).eulerAngles.y ,0f);
            }
            
            switch (animationType)
            {
                case PlayerCastAnimations.Cast:
                    _playerFsm.SetState<PlayerFsmStateCast>();
                    break;
                
                case PlayerCastAnimations.Summon:
                    _playerFsm.SetState<PlayerFsmStateSummon>();
                    break;
            }
        }
    }
}