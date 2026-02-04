using System;
using UnityEngine;

namespace Project.Scripts.Modifs
{
    public sealed class TripleShotModifier : ISpellModifier, IPriority
    {
        public int Priority => -100;

        public float SpreadAngleDeg { get; set; } = 12f;

        public void Apply(ISpell spell)
        {
            var inner = spell.CastAction;

            spell.CastAction = () =>
            {
                inner?.Invoke();

                var origin = spell.Origin ?? throw new Exception("Origin is null");

                var forward = origin.forward;
                var up = origin.up;

                var left = Quaternion.AngleAxis(-SpreadAngleDeg, up) * forward;
                var right = Quaternion.AngleAxis(SpreadAngleDeg, up) * forward;

                spell.SpawnProjectile(left);
                spell.SpawnProjectile(right);
            };
        }

        public void Dispose() { }
    }
}