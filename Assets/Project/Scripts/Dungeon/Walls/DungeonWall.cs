using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Project.Scripts.Walls
{
    [Serializable]
    public class DungeonWall : MonoBehaviour
    {
        [Header("Settings")] 
        [SerializeField] private float _fadeDuration = 0.5f;
        
        [Space]
        [SerializeField] private MeshRenderer _upperPartMeshRenderer;
        
        [Button]
        public void SetUpperPartTransparent(bool condition)
        {
            var finalAlpha = condition ? 1f : 0f;
            
            _upperPartMeshRenderer.material.DOFade(finalAlpha, 0.5f);
        }
    }
}