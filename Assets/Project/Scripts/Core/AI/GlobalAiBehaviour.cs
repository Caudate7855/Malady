using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.Core
{
    public abstract class GlobalAiBehaviour : MonoBehaviour
    {
        private const float CYCLE_DELAY = 3.0f;
        
        public bool IsOpponentInAttackDistance;
        public bool IsOpponentInFollowDistance;

        [SerializeField] protected bool IsAiEnabled = true;

        protected AiBehaviourBase AiBehaviourBase { get; set; }
        protected GameObject TargetObject { get; set; }
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
                yield return new WaitForSeconds(CYCLE_DELAY);
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
                AiBehaviourBase.MoveTo(TargetObject.transform);
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

        public virtual void SetMoveToBehaviour(Transform targetTransform)
        {
            AiBehaviourBase.MoveTo(targetTransform);
        }
    }
}