using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Modifs
{
    public sealed class SinMoveModifier : ISpellModifier
    {
        public int Priority => 0;

        public float Amplitude { get; set; } = 0.25f;
        public float Frequency { get; set; } = 7f;

        private ISpell _spell;
        private readonly Dictionary<GameObject, float> _prev = new();
        private readonly List<GameObject> _toRemove = new();

        public void Apply(ISpell spell)
        {
            _spell = spell ?? throw new Exception("spell is null");

            var inner = _spell.TickAction;

            _spell.TickAction = dt =>
            {
                inner(dt);

                var t = _spell.Time;

                for (var i = 0; i < _spell.ActiveCount; i++)
                {
                    var go = _spell.GetInstance(i);
                    if (go == null) continue;

                    var phase = i * 0.6f;
                    var s = Mathf.Sin((t + phase) * Frequency);

                    if (!_prev.TryGetValue(go, out var prevS))
                        prevS = s;

                    _prev[go] = s;

                    var delta = (s - prevS) * Amplitude;
                    _spell.AddWorldOffset(i, Vector3.up * delta);
                }

                CleanupDead();
            };
        }

        public void Dispose()
        {
            _prev.Clear();
            _spell = null;
        }

        private void CleanupDead()
        {
            if (_prev.Count == 0) return;

            _toRemove.Clear();

            foreach (var kv in _prev)
            {
                if (kv.Key == null)
                    _toRemove.Add(kv.Key);
            }

            for (var i = 0; i < _toRemove.Count; i++)
                _prev.Remove(_toRemove[i]);
        }
    }
}