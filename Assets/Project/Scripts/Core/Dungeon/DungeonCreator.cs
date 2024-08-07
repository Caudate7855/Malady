using UnityEngine;

namespace Project.Scripts.Core.Dungeon
{
    public class DungeonCreator : MonoBehaviour
    {
        [SerializeField] private Dungeon _dungeon;

        private void Start()
        {
            Instantiate(_dungeon, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}