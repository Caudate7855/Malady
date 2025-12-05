using UnityEngine;

namespace Project.Scripts
{
    public abstract class AiBehaviourBase : MonoBehaviour
    {
        public virtual void Idle() { }
        public virtual void MoveTo(Transform targetTransform) { }
        public virtual void MoveToPlayer() {}
        public virtual void Patrol() { }

        public virtual void RotateToPoint(Vector3 point) { }
        public virtual void Attack() { }
    }
}