using System;
using Project.Scripts.Configs;
using Project.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [Serializable]
    public abstract class SpellBase : ISpell
    {
        public abstract SpellType SpellType { get; set; }
        public Transform Origin { get; set; }
        public PlayerCastAnimationType AnimationType { get; set; }
        public SpellElementType SpellElementType { get; set; }

        public float Time => _time;

        public abstract int ActiveCount { get; }

        public Action CastAction { get; set; }
        public Action<float> TickAction { get; set; }

        public Action<GameObject, int> ProjectileSpawned { get; set; }
        public Action<Vector3, Vector3, int> ProjectileExpired { get; set; }

        protected GameObject ViewPrefab { get; }
        protected SpellConfig SpellConfig { get; }

        protected float Speed { get; set; }
        protected float LifeTime { get; set; }

        [Inject] protected DiContainer DiСontainer;
        [Inject] protected MouseController MouseController;

        private float _time;

        protected SpellBase(GameObject viewPrefab, SpellConfig config)
        {
            ViewPrefab = viewPrefab;
            SpellConfig = config;

            SpellElementType = config.ElementType;
            Speed = config.Speed;
            LifeTime = config.LifeTime;

            AnimationType = config.AnimationType;

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

        public abstract GameObject GetInstance(int index);
        public abstract void AddWorldOffset(int index, Vector3 offset);

        protected abstract void DefaultCast();

        protected virtual void DefaultTick(float dt)
        {
        }
    }
}