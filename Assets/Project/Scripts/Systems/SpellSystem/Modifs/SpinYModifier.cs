using UnityEngine;

namespace Project.Scripts.Modifs
{
    public sealed class SpinYModifier : ISpellModifier
    {
        public float DegreesPerSecond { get; set; } = 360f;

        public void Apply(ISpell spell)
        {
            var inner = spell.TickAction;

            spell.TickAction = dt =>
            {
                inner(dt);

                for (var i = 0; i < spell.ActiveCount; i++)
                {
                    var go = spell.GetInstance(i);
                    if (go == null) continue;

                    go.transform.Rotate(0f, DegreesPerSecond * dt, 0f, Space.Self);
                }
            };
        }

        public void Dispose() { }
    }
}