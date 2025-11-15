using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class ProjectileBase : MonoBehaviour
    {
        [SerializeField] protected GameObject View;
        public virtual float Speed { get; protected set; } = 10f;
        public virtual float MaxLifeTime { get; protected set; } = 3f;

        private Vector3 _direction;
        private EnemyBase _target;

        public virtual void Initialize(Vector3 direction, EnemyBase target = default)
        {
            _direction = direction;
            _target = target;

            StartLogic();
        }

        private void OnTriggerEnter(Collider other)
        {
            other.TryGetComponent<EnemyBase>(out var result);

            if(result != null)
            {
                DealDamage(result);
                Destroy(gameObject);
            }
            
            if( _target != null )
            {
                DealDamage(result);
                Destroy(gameObject);
            }
        }

        private async void StartLogic()
        {
            var token = this.GetCancellationTokenOnDestroy();
            var currentLifeTime = 0f;

            try
            {
                while (currentLifeTime < MaxLifeTime)
                {
                    transform.position += _direction.normalized * Speed * Time.deltaTime;
                    currentLifeTime += Time.deltaTime;
                    await UniTask.WaitForEndOfFrame(token);
                }
            }
            catch (OperationCanceledException)
            {
                
            }

            if (this != null)
            {
                Destroy(gameObject);
            }
        }

        public void DealDamage(EnemyBase enemy)
        {
            enemy.TakeDamage(10);
        }
    }
}