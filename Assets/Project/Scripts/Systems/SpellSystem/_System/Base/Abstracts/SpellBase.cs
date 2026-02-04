using System;
using System.Collections.Generic;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    public abstract class SpellBase : ISpell
    {
        public Transform Origin { get; set; }
        public PlayerCastAnimations AnimationType { get; set; }

        public float Time => _time;
        public int ActiveCount => _instances.Count;

        public Action CastAction { get; set; }
        public Action<float> TickAction { get; set; }

        public Action<GameObject, int> ProjectileSpawned { get; set; }
        public Action<Vector3, Vector3, int> ProjectileExpired { get; set; }

        protected GameObject ViewPrefab { get; }
        protected SpellConfig SpellConfig { get; }

        protected float Speed { get; set; }
        protected float LifeTime { get; set; }

        private readonly List<GameObject> _instances = new();
        private readonly List<Vector3> _velocities = new();
        private readonly List<float> _lifetimes = new();
        private readonly List<byte> _canTriggerExpire = new();

        private float _time;

        protected SpellBase(GameObject viewPrefab, SpellConfig config)
        {
            ViewPrefab = viewPrefab;
            SpellConfig = config;

            Speed = config.Speed;
            LifeTime = config.LifeTime;
        
            CastAction = DefaultCast;
            TickAction = DefaultTick;
        }

        public void Cast()
        {
            CastAction?.Invoke();
        }

        public void Tick(float dt)
        {
            _time += dt;
            TickAction?.Invoke(dt);
        }

        public int SpawnProjectile(Vector3 direction, bool canTriggerExpire = true)
        {
            return SpawnProjectileAt(Origin.position, direction, canTriggerExpire);
        }

        public int SpawnProjectileAt(Vector3 position, Vector3 direction, bool canTriggerExpire = true)
        {
            var dir = direction.sqrMagnitude > 0f ? direction.normalized : Vector3.forward;
            var rot = Quaternion.LookRotation(dir, Vector3.up);
            var go = UnityEngine.Object.Instantiate(ViewPrefab, position, rot);

            var i = _instances.Count;
            _instances.Add(go);
            _velocities.Add(dir * Speed);
            _lifetimes.Add(LifeTime);
            _canTriggerExpire.Add(canTriggerExpire ? (byte)1 : (byte)0);

            ProjectileSpawned?.Invoke(go, i);

            return i;
        }

        public GameObject GetInstance(int index) => _instances[index];

        public void AddWorldOffset(int index, Vector3 offset)
        {
            var go = _instances[index];
            if (go == null) return;
            go.transform.position += offset;
        }

        protected virtual void DefaultCast()
        {
            SpawnProjectile(Origin.forward);
        }

        protected virtual void DefaultTick(float dt)
        {
            for (var i = _instances.Count - 1; i >= 0; i--)
            {
                var go = _instances[i];

                if (go == null)
                {
                    RemoveAt(i);
                    continue;
                }

                var t = _lifetimes[i] - dt;
                _lifetimes[i] = t;

                if (t <= 0f)
                {
                    if (_canTriggerExpire[i] == 1)
                    {
                        var pos = go.transform.position;
                        var dir = _velocities[i].sqrMagnitude > 0f ? _velocities[i].normalized : go.transform.forward;
                        ProjectileExpired?.Invoke(pos, dir, i);
                    }

                    UnityEngine.Object.Destroy(go);
                    RemoveAt(i);
                    continue;
                }

                go.transform.position += _velocities[i] * dt;
            }
        }

        private void RemoveAt(int index)
        {
            _instances.RemoveAt(index);
            _velocities.RemoveAt(index);
            _lifetimes.RemoveAt(index);
            _canTriggerExpire.RemoveAt(index);
        }
    }
}