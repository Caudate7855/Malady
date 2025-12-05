using Project.Scripts.Abstracts;
using UnityEngine;

namespace Project.Scripts.Services
{
    public class MouseTarget
    {
        public MouseTargetType MouseTargetType;
        public Vector3 TargetPosition;
        public InteractableBase Interactable;
        public EnemyBase Enemy;
    }
}