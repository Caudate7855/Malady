using System;
using Project.Scripts.Abstracts;
using UnityEngine;

namespace Project.Scripts
{
    public class WorldDropItem : InteractableBase
    {
        public event Action<WorldDropItem> PickedUp;

        public ItemData ItemData { get; private set; }
        public Sprite Sprite { get; private set; }

        private bool _picked;

        public void Setup(ItemData itemData, Sprite sprite)
        {
            ItemData = itemData;
            Sprite = sprite;
        }

        public override void Interact()
        {
            if (_picked)
            {
                return;
            }

            _picked = true;
            PickedUp?.Invoke(this);
        }
    }
}