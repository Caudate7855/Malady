using UnityEngine;

namespace Project.Scripts.Core.Hub
{
    public class HubCreator : MonoBehaviour
    {
        [SerializeField] private HubController _hubController;

        private void Start()
        {
            Instantiate(_hubController, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}