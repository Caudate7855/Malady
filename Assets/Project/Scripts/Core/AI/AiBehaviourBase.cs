using UnityEngine;

namespace Project.Scripts.Core
{
    public class AiBehaviourBase : MonoBehaviour
    {
        public virtual void Idle() { }
        public virtual void Move() { }
        public virtual void Patrol() { }
        public virtual void Attack() { }
    }
}