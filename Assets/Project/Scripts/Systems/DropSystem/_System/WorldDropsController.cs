using System;
using System.Collections.Generic;
using Itibsoft.PanelManager;
using Project.Scripts.Player;
using Zenject;

namespace Project.Scripts
{
    public class WorldDropsController : IInitializable, IDisposable
    {
        private readonly DropSystem _dropSystem;
        private readonly PlayerController _playerController;
        private readonly IPanelManager _panelManager;

        private InventoryController _inventoryController;

        private readonly List<WorldDropItem> _wired = new();

        public WorldDropsController(DropSystem dropSystem, PlayerController playerController, IPanelManager panelManager)
        {
            _dropSystem = dropSystem;
            _playerController = playerController;
            _panelManager = panelManager;
        }

        public void Initialize()
        {
            _inventoryController = _panelManager.LoadPanel<InventoryController>();
        }

        public void Register(WorldDropItem drop)
        {
            if (drop == null)
            {
                return;
            }

            _wired.Add(drop);

            _dropSystem.SpawnFollow(drop.Sprite, drop.transform, () =>
            {
                if (drop == null)
                {
                    return;
                }

                _playerController.TryMoveToPoint(drop.transform.position, drop, true);
            });

            drop.PickedUp += OnPickedUp;
        }

        private void OnPickedUp(WorldDropItem drop)
        {
            if (drop == null)
            {
                return;
            }

            var added = _inventoryController != null && _inventoryController.TryAddItem(drop.ItemData);
            if (!added)
            {
                return;
            }

            drop.PickedUp -= OnPickedUp;

            _dropSystem.Despawn(drop.transform);
            UnityEngine.Object.Destroy(drop.gameObject);
        }

        public void Dispose()
        {
            for (var i = 0; i < _wired.Count; i++)
            {
                var d = _wired[i];
                if (d == null)
                {
                    continue;
                }

                d.PickedUp -= OnPickedUp;
            }

            _wired.Clear();
        }
    }
}