using JetBrains.Annotations;
using Project.Scripts.Core;
using Project.Scripts.Core.Abstracts;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Project.Scripts
{
    [UsedImplicitly]
    public class MouseController : ITickable
    {
        [Inject] private MouseFsm _mouseFsm;
        
        private Camera _mainCamera;

        private int _clickableLayerMaskIndex;
        private int _groundLayerMaskIndex;
        private int _attackableLayerMaskIndex;
        
        
        private bool _isInitialized;

        public MouseTarget MouseTarget { get; } = new();

        public void Initialize()
        {
            _mainCamera = Object.FindFirstObjectByType<Camera>();
            _mouseFsm.Initialize();

            _clickableLayerMaskIndex = LayerMask.GetMask("Clickable");
            _groundLayerMaskIndex = LayerMask.GetMask("Ground");
            _attackableLayerMaskIndex = LayerMask.GetMask("Attackable");

            _isInitialized = true;
        }

        public void Tick()
        {
            if (!_isInitialized)
            {
                return;
            }

            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            
            if(Physics.Raycast(ray, out var raycastHit, 100f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Collide))
            {
                int hitLayer = raycastHit.transform.gameObject.layer;
                
                // Сравнение масок.
                // 1 << hitLayer - превращение индекса слоя в битовую маску
                // & - Побитовое сравнение
                // !=0 - Слой входит в указанный LayerMask
                
                if (((1 << hitLayer) & _clickableLayerMaskIndex) != 0) 
                {
                    _mouseFsm.SetState<OverInteractableMouseState>();
                    
                    UpdateMouseTarget(MouseTargetType.Interactable, 
                        raycastHit.transform.position, 
                        raycastHit.collider.gameObject.GetComponent<InteractableBase>());
                    
                    return;
                }

                if (((1 << hitLayer) & _attackableLayerMaskIndex) != 0)
                {
                    _mouseFsm.SetState<OverAttackableMouseState>();
                    
                    UpdateMouseTarget(MouseTargetType.Attackable, 
                        raycastHit.transform.position,
                        null,
                        raycastHit.transform.gameObject.GetComponent<EnemyBase>());
                    return;
                }
                
                if (((1 << hitLayer) & _groundLayerMaskIndex) != 0 || hitLayer == 0)
                {
                    _mouseFsm.SetState<DefaultMouseState>();
                    
                    UpdateMouseTarget(MouseTargetType.Ground, 
                        raycastHit.point);
                }
            }
        }

        public Vector3 GetGroundPosition()
        {
            if (!_isInitialized)
            {
                return default;
            }
            
            var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var raycastHit, 100f, Physics.DefaultRaycastLayers,
                    QueryTriggerInteraction.Ignore))
            {
                return raycastHit.point;
            }

            return default;
        }
        
        private void UpdateMouseTarget(MouseTargetType mouseTargetType, Vector3 targetPosition, InteractableBase interactable = null, EnemyBase attackable = null)
        {
            MouseTarget.MouseTargetType = mouseTargetType;
            MouseTarget.TargetPosition = targetPosition;
            MouseTarget.Interactable = interactable;
            MouseTarget.Enemy = attackable;
        }
    }
}