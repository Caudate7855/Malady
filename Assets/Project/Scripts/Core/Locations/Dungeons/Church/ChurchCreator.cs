using UnityEngine;

namespace Project.Scripts.Core
{
    public class ChurchCreator: MonoBehaviour
    {
        [SerializeField] private ChurchController _churchController;

        private void Start()
        {
            Instantiate(_churchController, new Vector3(0,0,0), Quaternion.identity);
        }
    }
}