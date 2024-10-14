using UnityEngine;

namespace Project.Scripts.Overlays.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private Vector2 _position;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _position = _rectTransform.position;
        }
    }
}