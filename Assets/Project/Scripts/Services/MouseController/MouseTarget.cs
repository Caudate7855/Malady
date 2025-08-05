using Project.Scripts.Core;
using Project.Scripts.Core.Abstracts;
using UnityEngine;

namespace Project.Scripts
{
    public class MouseTarget
    {
        public MouseTargetType MouseTargetType;
        public Vector3 TargetPosition;
        public InteractableBase Interactable;
        public EnemyBase Enemy;
    }
}