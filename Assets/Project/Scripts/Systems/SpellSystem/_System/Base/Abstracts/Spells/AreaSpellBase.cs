using System;
using Project.Scripts.Configs;
using UnityEngine;

namespace Project.Scripts
{
    [Serializable]
    public abstract class AreaSpellBase : ProjectileSpellBase
    {
        public override SpellType SpellType { get; set; } = SpellType.Area;

        public float Radius { get; private set; } = 1f;

        public Vector3 AreaCenterWorld { get; private set; }
        public bool HasAreaCenter { get; private set; }

        protected AreaSpellBase(GameObject viewPrefab, SpellConfig config) : base(viewPrefab, config)
        {
            Speed = 0f;
        }

        public void SetArea(Vector3 centerWorld, float radius)
        {
            AreaCenterWorld = centerWorld;
            HasAreaCenter = true;
            Radius = radius;
        }

        protected override void DefaultCast()
        {
            var origin = Origin ?? throw new Exception("Origin is null");

            var pos = HasAreaCenter ? AreaCenterWorld : origin.position;

            var i = SpawnInternal(pos, origin.forward);

            var go = GetInstance(i);
            if (go == null) return;

            ApplyRadius(go, Radius);
        }

        protected virtual void ApplyRadius(GameObject go, float radius)
        {
            var d = Mathf.Max(0.0001f, radius * 2f);
            var s = go.transform.localScale;
            go.transform.localScale = new Vector3(d, s.y, d);
        }
    }
}