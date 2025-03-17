using UnityEngine;

namespace Project.Scripts.Core
{
    public class SanBoxControllerCreator : MonoBehaviour
    {
        [SerializeField] private SandBoxController _sandBoxController;

        private void Start()
        {
            Instantiate(_sandBoxController, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}