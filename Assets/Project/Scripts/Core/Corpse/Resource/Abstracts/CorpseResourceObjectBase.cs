using System;
using UnityEngine;

namespace Project.Scripts.Core
{
    public class CorpseResourceObjectBase : MonoBehaviour
    {
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void SwitchState(bool condition)
        {
            gameObject.SetActive(condition);
        }
    }
}