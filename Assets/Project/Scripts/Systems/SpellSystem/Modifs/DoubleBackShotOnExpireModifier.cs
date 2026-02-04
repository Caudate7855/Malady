using System;
using UnityEngine;

namespace Project.Scripts.Modifs
{
    public sealed class DoubleBackShotOnExpireModifier : ISpellModifier
    {
        public int Priority => 0;

        public float SpreadAngleDeg { get; set; } = 12f;

        private ISpell _spell;
        private Action<Vector3, Vector3, int> _expired;

        public void Apply(ISpell spell)
        {
            _spell = spell ?? throw new Exception("spell is null");

            _expired = OnExpired;
            _spell.ProjectileExpired -= _expired;
            _spell.ProjectileExpired += _expired;
        }

        public void Dispose()
        {
            if (_spell != null && _expired != null)
                _spell.ProjectileExpired -= _expired;

            _expired = null;
            _spell = null;
        }

        private void OnExpired(Vector3 position, Vector3 dir, int index)
        {
            var back = -dir;
            var left = Quaternion.AngleAxis(-SpreadAngleDeg, Vector3.up) * back;
            var right = Quaternion.AngleAxis(SpreadAngleDeg, Vector3.up) * back;

            _spell.SpawnProjectileAt(position, left, false);
            _spell.SpawnProjectileAt(position, right, false);
        }
    }
}