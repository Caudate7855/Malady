using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core
{
    public abstract class EnemyBase : MonoBehaviour, IEnemy, ICustomInitializable
    {
        public IPlayer Player { get; set; }

        public void Initialize()
        {
            Player = FindObjectOfType<PlayerController>();
        }
    }
}