using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    public abstract class ProjectileBase : MonoBehaviour
    {
        public float Speed => _speed;
        public float MaxLifeTime => _maxLifeTime;

        [Header("Settings")]
        [SerializeField] protected float _speed = 10f;
        [SerializeField] protected float _maxLifeTime = 3f;
        
        [Space, Header("View Prefab")]
        [SerializeField] protected GameObject View;
        [Inject] protected PlayerStats PlayerStats;


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

            if (result != null)
            {
                DealDamage(result);
                Destroy(gameObject);
            }

            if (_target != null)
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
            var damage = CalculateDamage();
            enemy.TakeDamage(damage);
        }

        protected abstract float CalculateDamage();
    }
}