using System;
using UnityEngine;

namespace Project.Scripts.Modifs
{
    public class TripleShotModifier : ISpellModifier, IPriority
    {
        public int Priority => -100;

        public float SpreadAngleDeg { get; set; } = 12f;

        public void Apply(ISpell spell)
        {
            if (spell == null) throw new Exception("spell is null");

            var inner = spell.CastAction;

            spell.CastAction = () =>
            {
                var origin = spell.Origin ?? throw new Exception("Origin is null");

                var baseRot = origin.rotation;

                try
                {
                    origin.rotation = baseRot;
                    inner?.Invoke();

                    var up = origin.up;

                    origin.rotation = Quaternion.AngleAxis(-SpreadAngleDeg, up) * baseRot;
                    inner?.Invoke();

                    origin.rotation = Quaternion.AngleAxis(SpreadAngleDeg, up) * baseRot;
                    inner?.Invoke();
                }
                finally
                {
                    origin.rotation = baseRot;
                }
            };
        }

        public void Dispose() { }
    }
}