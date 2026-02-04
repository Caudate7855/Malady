using System;
using UnityEngine;

namespace Project.Scripts
{
    public sealed class Entry
    {
        public Transform World;
        public DropItemUIView View;

        public Vector2 Projected;
        public Vector2 Desired;
        public Vector2 Size;

        public Action OnClick;
    }
}