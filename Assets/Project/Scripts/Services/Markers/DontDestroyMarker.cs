using UnityEngine;

namespace Project.Scripts
{
    public class DontDestroyMarker : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}