using System;
using System.Collections.Generic;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public abstract class SummonSpellBase : SpellBase
    {
        public override SpellType SpellType { get; set; } = SpellType.Summon;
        
        private readonly List<GameObject> _instances = new();
        private readonly List<float> _lifetimes = new();

        public int Count { get; private set; } = 1;

        public override int ActiveCount => _instances.Count;

        protected SummonSpellBase(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            Speed = 0f;
        }

        public void SetCount(int count)
        {
            Count = Mathf.Max(1, count);
        }

        public override GameObject GetInstance(int index) => _instances[index];

        public override void AddWorldOffset(int index, Vector3 offset)
        {
            var go = _instances[index];
            if (go == null) return;
            go.transform.position += offset;
        }

        protected override void DefaultCast()
        {
            if (MouseController == null) throw new Exception("MouseController is null");

            var pos = MouseController.GetGroundPosition();
            if (pos == default) throw new Exception("MouseController.GetGroundPosition returned default");

            var prefab = ViewPrefab.GetComponent<SummonUnitBase>();
            if (prefab == null) throw new Exception("ViewPrefab has no SummonUnitBase");

            var n = Mathf.Max(1, Count);
            for (var k = 0; k < n; k++)
            {
                var unit = DiСontainer.InstantiatePrefabForComponent<SummonUnitBase>(prefab, pos, Quaternion.identity, null);
                var go = unit.gameObject;

                var i = _instances.Count;
                _instances.Add(go);
                _lifetimes.Add(LifeTime);

                ProjectileSpawned?.Invoke(go, i);
            }
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

                if (LifeTime <= 0f) continue;

                var t = _lifetimes[i] - dt;
                _lifetimes[i] = t;

                if (t > 0f) continue;

                var pos = go.transform.position;
                var dir = go.transform.forward;

                ProjectileExpired?.Invoke(pos, dir, i);

                UnityEngine.Object.Destroy(go);
                RemoveAt(i);
            }
        }

        private void RemoveAt(int index)
        {
            _instances.RemoveAt(index);
            _lifetimes.RemoveAt(index);
        }
    }
}
