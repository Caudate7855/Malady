using Itibsoft.PanelManager;
using Project.Scripts.Abstracts;
using Project.Scripts.Services;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Scripts
{
    public class PlayerController : MonoBehaviour, IPlayer, IInitializable
    {
        [Inject] private IPanelManager _panelManager;
        [Inject] private PlayerMover _playerMover;
        [Inject] private PlayerFsm _playerFsm;
        [Inject] private PlayerStats _playerStats;
        [Inject] private MouseController _mouseController;

        private MainUIController  _mainUIController;

        public void Initialize()
        {
            _mainUIController = _panelManager.LoadPanel<MainUIController>();
            
            _mainUIController.UpdateBar<HpBar>(_playerStats.GetStat<HpStat>().Value, _playerStats.GetStat<HpStat>().MaxValue);
            _mainUIController.UpdateBar<EssenceBar>(_playerStats.GetStat<EssenceStat>().Value,  _playerStats.GetStat<EssenceStat>().MaxValue);

            _playerMover.OnDestinationReached += Idle;

            _playerFsm.Initialize(GetComponent<NavMeshAgent>(), GetComponentInChildren<Animator>());
            _playerStats.Initialize();
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
            
            var direction = (_mouseController.GetGroundPosition());
            direction.y = 0; 
            direction.Normalize();
            
            transform.rotation = Quaternion.LookRotation(_mouseController.GetGroundPosition());
            
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