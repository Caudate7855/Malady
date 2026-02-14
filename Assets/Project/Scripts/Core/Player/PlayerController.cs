using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Abstracts;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Scripts.Player
{
    public class PlayerController : MonoBehaviour, IInitializable
    {
        [Inject] private IPanelManager _panelManager;
        [Inject] private PlayerMover _playerMover;
        [Inject] private PlayerFsm _playerFsm;
        [Inject] private StatSystem _statSystem;
        [Inject] private MouseController _mouseController;

        private MainUIController _mainUIController;
        private NavMeshAgent _navMeshAgent;

        private static readonly List<RaycastResult> _uiHits = new(32);
        private static PointerEventData _pointerEventData;

        public void Initialize()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _mainUIController = _panelManager.LoadPanel<MainUIController>();

            _playerFsm.Initialize(_navMeshAgent, GetComponentInChildren<Animator>());
            _statSystem.Initialize();

            _playerMover.SetNavMeshAgent(_navMeshAgent);
            _playerMover.ClearOnDestinationReached();
            _playerMover.OnDestinationReached += Idle;
        }

        public bool IsNavMeshAgentReady()
        {
            return _navMeshAgent != null && _navMeshAgent.enabled;
        }

        public void TryMoveToPoint(Vector3 targetLocation, InteractableBase interactable = default, bool ignoreUi = false)
        {
            if (_navMeshAgent == null)
            {
                _navMeshAgent = GetComponent<NavMeshAgent>();
            }

            if (_navMeshAgent == null || !_navMeshAgent.enabled)
            {
                Debug.Log("TryMoveToPoint: navmesh agent not ready");
                return;
            }

            _playerMover.SetNavMeshAgent(_navMeshAgent);

            if (!ignoreUi && IsPointerOverUi())
            {
                return;
            }

            if (!_playerFsm.IsPossibleToMove)
            {
                Debug.Log("TryMoveToPoint: IsPossibleToMove == false");
                return;
            }

            _playerMover.ClearOnDestinationReached();

            if (interactable != null)
            {
                var local = interactable;
                _playerMover.OnDestinationReached += () => Interact(local);
                _playerMover.OnDestinationReached += Idle;
            }
            else
            {
                _playerMover.OnDestinationReached += Idle;
            }

            ContinueMovement();
            _playerMover.MoveToPoint(targetLocation);
            _playerFsm.SetState<PlayerFsmStateRun>();
            _playerFsm.GetCurrentState().Update();
        }

        private bool IsPointerOverUi()
        {
            var es = EventSystem.current;
            if (es == null)
            {
                return false;
            }

            if (es.IsPointerOverGameObject())
            {
                return true;
            }

            if (_pointerEventData == null)
            {
                _pointerEventData = new PointerEventData(es);
            }

            _pointerEventData.position = Input.mousePosition;

            _uiHits.Clear();
            es.RaycastAll(_pointerEventData, _uiHits);

            return _uiHits.Count > 0;
        }

        private void StopMovement()
        {
            _playerMover.SetNavMeshAgent(_navMeshAgent);
            _playerMover.StopMovement();
        }

        private void ContinueMovement()
        {
            _playerMover.SetNavMeshAgent(_navMeshAgent);
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

        public void PlayCastAnimation(PlayerCastAnimationType animationTypeType)
        {
            StopMovement();

            var dir = _mouseController.GetGroundPosition() - transform.position;
            dir.y = 0f;

            if (dir != Vector3.zero)
            {
                transform.rotation = Quaternion.Euler(0f, Quaternion.LookRotation(dir).eulerAngles.y, 0f);
            }

            switch (animationTypeType)
            {
                case PlayerCastAnimationType.Cast:
                    _playerFsm.SetState<PlayerFsmStateCast>();
                    break;

                case PlayerCastAnimationType.Summon:
                    _playerFsm.SetState<PlayerFsmStateSummon>();
                    break;
            }
        }
    }
}