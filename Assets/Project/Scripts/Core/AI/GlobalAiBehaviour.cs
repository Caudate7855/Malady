using System.Collections;
using UnityEngine;
using Random = System.Random;

namespace Project.Scripts.Core
{
    public class GlobalAiBehaviour : MonoBehaviour
    {
        private const float CYCLE_DELAY = 3.0f;

        private AiBehaviourBase _aiBehaviourBase;
        private bool _isAlive;
        private Random _random = new();

        private void Awake()
        {
            _aiBehaviourBase = GetComponent<AiBehaviourBase>();
        }

        private void Start()
        {
            StartCoroutine(BehaviourCycle());
        }

        private IEnumerator BehaviourCycle()
        {
            while (_isAlive)
            {
                TryChangeBehaviour();
                yield return new WaitForSeconds(CYCLE_DELAY);
            }
        }

        private void TryChangeBehaviour()
        {
            if (_aiBehaviourBase.IsOpponentInAttackDistance)
            {
                SetAttackRandomBehaviour();
                return;
            }

            if (_aiBehaviourBase.IsOpponentInFollowDistance)
            {
                SetFollowRandomBehaviour();
            }

            if (!_aiBehaviourBase.IsOpponentInFollowDistance)
            {
                
            }
        }

        private void SetDefaultRandomBehaviour()
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

        private void SetFollowRandomBehaviour()
        {
            var randomBehaviourIndex = _random.Next(0, 10 + 1);

            if (randomBehaviourIndex <= 2)
            {
                _aiBehaviourBase.Idle();
            }
            else
            {
                _aiBehaviourBase.Move();
            }
        }
        
        private void SetAttackRandomBehaviour()
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
    }
}