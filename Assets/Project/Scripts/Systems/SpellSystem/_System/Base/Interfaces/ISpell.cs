using System;
using Project.Scripts.Player;
using UnityEngine;

namespace Project.Scripts
{
    public interface ISpell
    {
        Transform Origin { get; set; }
        PlayerCastAnimationType AnimationType { get; set; }

        float Time { get; }
        int ActiveCount { get; }

        Action CastAction { get; set; }
        Action<float> TickAction { get; set; }

        Action<GameObject, int> ProjectileSpawned { get; set; }
        Action<Vector3, Vector3, int> ProjectileExpired { get; set; }

        void Cast();
        void Tick(float dt);

        GameObject GetInstance(int index);
        void AddWorldOffset(int index, Vector3 offset);
    }
}