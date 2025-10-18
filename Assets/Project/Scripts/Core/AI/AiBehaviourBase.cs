using UnityEngine;

namespace Project.Scripts.Core
{
    public abstract class AiBehaviourBase : MonoBehaviour
    {
        public abstract void Initialize();
        
        public virtual void Idle() { }
        public virtual void MoveTo(Transform targetTransform) { }
        public virtual void MoveToPlayer() {}
        public virtual void Patrol() { }
        public virtual void Attack() { }
    }
}