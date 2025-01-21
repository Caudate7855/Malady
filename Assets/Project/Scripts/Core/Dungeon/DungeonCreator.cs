using UnityEngine;
using UnityEngine.Serialization;

namespace Project.Scripts.Core.Dungeon
{
    public class DungeonCreator : MonoBehaviour
    {
        [SerializeField] private HubController _hubController;

        private void Start()
        {
            Instantiate(_hubController, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}