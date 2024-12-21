using UnityEngine;

namespace Project.Scripts.Services
{
    public class DontDestroyMarker : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}