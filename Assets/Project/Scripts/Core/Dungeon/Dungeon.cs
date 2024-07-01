using Project.Scripts.Interfaces;
using UnityEngine;

namespace Project.Scripts.Core.Dungeon
{
    public class Dungeon : MonoBehaviour, ICustomInitializable
    {
        public void Initialize()
        {
            Debug.Log("Created");
        }
    }
}