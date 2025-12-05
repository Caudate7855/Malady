using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Project.Scripts
{
    public abstract class GlobalAiBehaviour : MonoBehaviour
    {
        protected virtual float CycleDelay { get; set; } = 3f;

        public bool IsOpponentInAttackDistance;
        public bool IsOpponentInFollowDistance;

        [SerializeField] protected bool IsAiEnabled = true;

        protected AiBehaviourBase AiBehaviourBase { get; set; }
        protected GameObject FollowObject { get; set; }
        protected GameObject AttackObject { get; set; }
        private bool _isAlive = true;
        
        private Random _random = new();
        
        private void Awake()
        {
            AiBehaviourBase = GetComponent<AiBehaviourBase>();
        }

        private void Start()
        {
            Initialize();
            StartCoroutine(BehaviourCycle());
        }
        
        protected abstract void Initialize();

        private IEnumerator BehaviourCycle()
        {
            while (_isAlive && IsAiEnabled)
            {
                TryChangeBehaviour();
                yield return new WaitForSeconds(CycleDelay);
            }
        }

        protected virtual void TryChangeBehaviour()
        {
            if (IsOpponentInAttackDistance)
            {
                SetAttackBehaviour();
                return;
            }

            if (IsOpponentInFollowDistance)
            {
                SetFollowBehaviour();
                return;
            }

            SetIdleBehaviour();
        }

        public virtual void SetAttackBehaviour()
        {
            var randomBehaviourIndex = _random.Next(0, 10 + 1);

            if (randomBehaviourIndex <= 2)
            {
                AiBehaviourBase.Idle();
            }
            else
            {
                AiBehaviourBase.Attack();
            }
        }

        public virtual void SetFollowBehaviour()
        {
            var randomBehaviourIndex = _random.Next(0, 10 + 1);

            if (randomBehaviourIndex <= 1)
            {
                AiBehaviourBase.Idle();
            }
            else
            {
                AiBehaviourBase.MoveTo(FollowObject.transform);
            }
        }

        public virtual void SetIdleBehaviour()
        {
            var randomBehaviourIndex = _random.Next(0, 10 + 1);

            if (randomBehaviourIndex <= 3)
            {
                AiBehaviourBase.Patrol();
            }
            else
            {
                AiBehaviourBase.Idle();
            }
        }
    }
}