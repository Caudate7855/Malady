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
        
        protected AiBehaviourBase _aiBehaviourBase;
        private bool _isAlive = true;
        private Random _random = new();
        
        private void Awake()
        {
            _aiBehaviourBase = GetComponent<AiBehaviourBase>();
        }

        private void Start()
        {
            Initialize();
            StartCoroutine(BehaviourCycle());
        }
        
        protected abstract void Initialize();

        private IEnumerator BehaviourCycle()
        {
            while (_isAlive)
            {
                TryChangeBehaviour();
                yield return new WaitForSeconds(CYCLE_DELAY);
            }
        }

        protected virtual void TryChangeBehaviour()
        {
            if (IsOpponentInAttackDistance)
            {
                SetAttackRandomBehaviour();
                return;
            }

            if (IsOpponentInFollowDistance)
            {
                SetFollowRandomBehaviour();
                return;
            }

            SetIdleRandomBehaviour();
        }

        public virtual void SetAttackRandomBehaviour()
        {
            var randomBehaviourIndex = _random.Next(0, 10 + 1);

            if (randomBehaviourIndex <= 2)
            {
                _aiBehaviourBase.Idle();
            }
            else
            {
                _aiBehaviourBase.Attack();
            }
        }

        public virtual void SetFollowRandomBehaviour()
        {
            var randomBehaviourIndex = _random.Next(0, 10 + 1);

            if (randomBehaviourIndex <= 1)
            {
                _aiBehaviourBase.Idle();
            }
            else
            {
                _aiBehaviourBase.Move();
            }
        }

        public virtual void SetIdleRandomBehaviour()
        {
            var randomBehaviourIndex = _random.Next(0, 10 + 1);

            if (randomBehaviourIndex <= 3)
            {
                _aiBehaviourBase.Patrol();
            }
            else
            {
                _aiBehaviourBase.Idle();
            }
        }
    }
}