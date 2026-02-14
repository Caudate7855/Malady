using UnityEngine;

namespace Project.Scripts
{
    public class ItemTip : MonoBehaviour
    {
        public void Init(Canvas canvas)
        {
            
        }

        public void SetInfo(ItemData itemData)
        {
            
            
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}