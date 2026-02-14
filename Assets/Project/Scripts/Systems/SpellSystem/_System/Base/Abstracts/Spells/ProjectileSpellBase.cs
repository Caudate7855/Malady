using System;
using System.Collections.Generic;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public abstract class ProjectileSpellBase : SpellBase
    {
        public override SpellType SpellType { get; set; } = SpellType.Projectile;
        
        private readonly List<GameObject> _instances = new();
        private readonly List<Vector3> _velocities = new();
        private readonly List<float> _lifetimes = new();
        private readonly List<byte> _canTriggerExpire = new();

        protected bool DisableExpireThisCast { get; private set; }

        public override int ActiveCount => _instances.Count;

        protected ProjectileSpellBase(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
        }

        public override GameObject GetInstance(int index) => _instances[index];

        public override void AddWorldOffset(int index, Vector3 offset)
        {
            var go = _instances[index];
            if (go == null) return;
            go.transform.position += offset;
        }

        protected int SpawnInternal(Vector3 position, Vector3 direction, bool canTriggerExpire = true)
        {
            var dir = direction.sqrMagnitude > 0f ? direction.normalized : Vector3.forward;
            var rot = Quaternion.LookRotation(dir, Vector3.up);

            var go = UnityEngine.Object.Instantiate(ViewPrefab, position, rot);

            var i = _instances.Count;

            _instances.Add(go);
            _velocities.Add(dir * Speed);
            _lifetimes.Add(LifeTime);
            _canTriggerExpire.Add((canTriggerExpire && DisableExpireThisCast == false) ? (byte)1 : (byte)0);

            ProjectileSpawned?.Invoke(go, i);

            return i;
        }

        protected int SpawnFromOriginForward(bool canTriggerExpire = true)
        {
            var origin = Origin ?? throw new Exception("Origin is null");
            return SpawnInternal(origin.position, origin.forward, canTriggerExpire);
        }

        protected override void DefaultTick(float dt)
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
                        var vel = _velocities[i];
                        var dir = vel.sqrMagnitude > 0f ? vel.normalized : go.transform.forward;
                        ProjectileExpired?.Invoke(pos, dir, i);
                    }

                    UnityEngine.Object.Destroy(go);
                    RemoveAt(i);
                    continue;
                }

                go.transform.position += _velocities[i] * dt;
            }
        }

        protected void WithDisableExpireThisCast(Action action)
        {
            var prev = DisableExpireThisCast;
            DisableExpireThisCast = true;

            try
            {
                action?.Invoke();
            }
            finally
            {
                DisableExpireThisCast = prev;
            }
        }

        public void CastFrom(Vector3 position, Vector3 forward, bool disableExpireThisCast = false)
        {
            var prevOrigin = Origin ?? throw new Exception("Origin is null");

            var tempGo = new GameObject("spell_origin_temp");
            var temp = tempGo.transform;

            var dir = forward.sqrMagnitude > 0f ? forward.normalized : Vector3.forward;

            temp.position = position;
            temp.rotation = Quaternion.LookRotation(dir, Vector3.up);

            try
            {
                Origin = temp;

                if (disableExpireThisCast)
                    WithDisableExpireThisCast(Cast);
                else
                    Cast();
            }
            finally
            {
                Origin = prevOrigin;
                UnityEngine.Object.Destroy(tempGo);
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
