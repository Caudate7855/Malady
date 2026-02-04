using UnityEngine;

namespace Project.Scripts.CorpseSystem.Resource.Abstracts
{
    public class CorpseResourceObjectBase : MonoBehaviour
    {
        public virtual ResourceType ResourceType { get; private set; }
        
        public void SwitchState(bool condition)
        {
            gameObject.SetActive(condition);
        }
    }
}