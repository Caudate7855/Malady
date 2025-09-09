using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class ProjectileBase : MonoBehaviour
    {
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

        private async void StartLogic()
        {
            var currentLifeTime = 0f;
            
            while (currentLifeTime < MaxLifeTime)
            {
                transform.position += _direction.normalized * Speed * Time.deltaTime;
                currentLifeTime += Time.deltaTime;
                await UniTask.WaitForEndOfFrame();
            }   
            
            Destroy(gameObject);
        }

        private void MoveForward()
        {
            
        }
    }
}