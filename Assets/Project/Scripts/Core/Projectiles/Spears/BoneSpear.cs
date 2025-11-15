using UnityEngine;

namespace Project.Scripts.Core
{
    public class BoneSpear : ProjectileBase
    {
        public override float Speed { get; protected set; } = 20f;

        public override void Initialize(Vector3 direction, EnemyBase target = default)
        {
            base.Initialize(direction, target);
        }
    }
}